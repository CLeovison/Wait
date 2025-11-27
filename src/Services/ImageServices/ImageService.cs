using System.Security.Claims;
using Microsoft.Extensions.Options;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Wait.Infrastructure.Common;
using Wait.Infrastructure.Configuration;
using Wait.Infrastructure.Repositories;
using Wait.Services.FileServices;

namespace Wait.Services.ImageServices;


public sealed class ImageService(
    IOptions<UploadDirectoryOptions> options,
    IHttpContextAccessor httpContext,
    IImageRepository imageRepository) : IImageService
{
    private readonly UploadDirectoryOptions settings = options.Value;

    public bool IsValidImage(IFormFile file)
    {
        if (file.Length == 0)
        {
            return false;
        }

        try
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return settings.AllowedExtensions.Contains(extension) && settings.AllowedMimeTypes.Contains(file.ContentType);
        }
        catch (Exception ex)
        {
            throw new Exception("Error validating image", ex);
        }
    }

    public async Task<string> SaveOriginalImageAsync(IFormFile file, string folderPath, string folderName, CancellationToken ct)
    {
        if (!IsValidImage(file))
        {
            throw new ArgumentException("Invalid image file", nameof(file));
        }

        try
        {
            var originalFilePath = Path.Combine(folderPath, folderName);

            Directory.CreateDirectory(folderPath);

            using var stream = new FileStream(originalFilePath, FileMode.Create);

            await file.CopyToAsync(stream, ct);

            return originalFilePath;
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving original image", ex);
        }
    }


    public async Task<IEnumerable<string>> GenerateThumbnailAsync(
        string originalFilePath,          // Path of the original image
        string folderPath,                // Where to save thumbnails
        string fileNameWithoutExtension,  // Base name for thumbnails
        int[]? widths = null)
    {

        var thumbnailPaths = new List<string>();

        var extension = Path.GetExtension(originalFilePath);

        widths ??= options.Value.ThumbnailsWidth;


        try
        {
            using var image = await Image.LoadAsync(originalFilePath);

            foreach (var width in widths)
            {
                var thumbnailFileName = $"{fileNameWithoutExtension}_w{width}{extension}";

                var thumbnailPath = Path.Combine(folderPath, thumbnailFileName);

                using var resizedImage = image.Clone(x => x.Resize(width, 0));

                await resizedImage.SaveAsync(thumbnailPath);

                thumbnailPaths.Add(thumbnailPath);
            }

            return thumbnailPaths;
        }
        catch (Exception ex)
        {
            throw new Exception("Error generating thumbnails", ex);
        }
    }
    public async Task<Stream> GetImageByIdAsync(string id, int? width, CancellationToken ct)
    {
        try
        {
            var image = await imageRepository.GetImageByIdAsync(id, ct);
            if (image is null)
                throw new FileNotFoundException("Image metadata not found");

            // Build absolute path using UploadFolder + objectKey
            var fullPath = Path.Combine(settings.UploadFolder, image.ObjectKey);

            // Thumbnail logic (if requested)
            if (width is not null)
            {
                var folder = Path.GetDirectoryName(fullPath)!;
                var ext = Path.GetExtension(fullPath);
                var baseName = Path.GetFileNameWithoutExtension(fullPath);

                var thumbPath = Path.Combine(folder, $"{baseName}_w{width}{ext}");
                if (File.Exists(thumbPath))
                    fullPath = thumbPath;
            }

            // Check file existence
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Image not found at {fullPath}");

            return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }
        catch (Exception ex)
        {
            throw new IOException("Error retrieving image", ex);
        }
    }

    public async Task<ImageResult> GetImageMetadataAsync(string id, CancellationToken ct)
    {
        try
        {
            var result = await imageRepository.GetImageByIdAsync(id, ct) 
            ?? throw new FileNotFoundException("Image metadata not found");
            
            return result;
        }
        catch (Exception ex)
        {
            throw new IOException("Error retrieving image metadata", ex);
        }


    }

    public async Task<ImageResult> UploadImageAsync(IFormFile file, CancellationToken ct)
    {
        if (!IsValidImage(file))
            throw new ArgumentException("Invalid image file");

        if (file.Length > settings.ImageFileSize)
            throw new InvalidOperationException("File size exceeds limit.");

        try
        {
            // 1. Generate ID and sanitized filename
            var imageId = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{imageId}{extension}";   // clean, stable filename

            // 2. Build relative object key (DB value)
            var objectKey = Path.Combine("images", imageId.ToString(), fileName)
                                .Replace("\\", "/");

            // 3. Build full physical path
            var physicalFolder = Path.Combine(settings.UploadFolder, "images", imageId.ToString());
            Directory.CreateDirectory(physicalFolder);

            var physicalPath = Path.Combine(physicalFolder, fileName);

            // 4. Save the file
            using (var stream = new FileStream(physicalPath, FileMode.Create))
                await file.CopyToAsync(stream, ct);

            // 5. Build public URL
            var ctx = httpContext.HttpContext!;
            var storageUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host}/uploads/{objectKey}";

            // 6. Ensure user identity is present
            var userIdString = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString is null)
                throw new InvalidOperationException("User ID not found in claims.");

            // 7. Create metadata
            var imageResult = new ImageResult
            {
                ImageId = imageId,
                ObjectKey = objectKey,                    // <<< relative key only
                StorageUrl = storageUrl,
                DateUploaded = DateTime.UtcNow,
                OriginalFileName = file.FileName,
                MimeType = file.ContentType,
                FileLength = file.Length,
                FileExtension = extension,
                UserId = Guid.Parse(userIdString)
            };

            return await imageRepository.UploadImageAsync(imageResult, ct);
        }
        catch (Exception ex)
        {
            throw new IOException("Error while uploading image", ex);
        }
    }

    public async Task<ImageOperationResult> DeleteImageAsync(string id, CancellationToken ct)
    {
        if (id is null)
        {
            throw new FileNotFoundException("The Image does not exist");
        }

        var filePath = Path.Combine(settings.UploadFolder, id);

        if (!Directory.Exists(filePath))
        {
            throw new DirectoryNotFoundException("The Directory was not existing");
        }


        try
        {

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Image Not Found {filePath}");
            }

            await Task.Run(() => File.Delete(filePath), ct);
            return new ImageOperationResult
            {
                Success = true,
                ImageName = id,
                Message = "Image deleted successfully."
            };

        }
        catch (Exception ex)
        {
            return new ImageOperationResult
            {
                Success = false,
                ImageName = id,
                Message = $"Error deleting image: {ex.Message}"
            };

            throw new FileNotFoundException("The File is already deleted", ex);

        }
    }


}
