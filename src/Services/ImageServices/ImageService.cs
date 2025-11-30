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

    public async Task<ImageResult> UploadImageAsync(IFormFile file, CancellationToken ct)
    {
        if (!IsValidImage(file))
            throw new ArgumentException("Invalid image file");

        if (file.Length > settings.ImageFileSize)
            throw new InvalidOperationException("File size exceeds limit.");

        try
        {
            
            var imageId = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{imageId}{extension}";   

          
            var objectKey = Path.Combine("images", imageId.ToString(), fileName)
                                .Replace("\\", "/");

            var physicalFolder = Path.Combine(settings.UploadFolder, "images", imageId.ToString());
            Directory.CreateDirectory(physicalFolder);

            var physicalPath = Path.Combine(physicalFolder, fileName);

            using var stream = new FileStream(physicalPath, FileMode.Create);
            await file.CopyToAsync(stream, ct);

            var ctx = httpContext.HttpContext!;
            var storageUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host}/uploads/{objectKey}";

            var userIdString = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString is null)
                throw new InvalidOperationException("User ID not found in claims.");

            var imageResult = new ImageResult
            {
                ImageId = imageId,
                ObjectKey = objectKey,
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

    public async Task<ImageResult> GetImageObjectKeyAsync(string objectKey, CancellationToken ct)
    {
        try
        {
            var request = await imageRepository.GetImageByObjectKeyAsync(objectKey, ct);

            if (request is null)
            {
                throw new FileNotFoundException("The Image Metadata does not exists");
            }

            return request;
        }
        catch (Exception ex)
        {
            throw new IOException("Error retrieving image metadata by objectKey", ex);
        }
    }

    public async Task<Stream> GetImageStreamAsync(ImageResult image, int? width, CancellationToken ct)
    {
        try
        {
            var normalizedKey = image.ObjectKey.Replace("/", Path.DirectorySeparatorChar.ToString());
            var fullPath = Path.Combine(settings.UploadFolder, normalizedKey);

            if (width is not null)
            {
                var folder = Path.GetDirectoryName(fullPath)!;
                var extension = Path.GetExtension(fullPath);
                var baseName = Path.GetFileNameWithoutExtension(fullPath);

                var thumbPath = Path.Combine(folder, $"{baseName}_w{extension}");

            }

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"Image not found at {fullPath}");
            }

            return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }
        catch (Exception ex)
        {
            throw new IOException("Error retrieving image stream", ex);
        }
    }


    public async Task<bool> DeleteImageAsync(string objectKey, CancellationToken ct)
    {
        try
        {
            var imageObject = await imageRepository.GetImageByObjectKeyAsync(objectKey, ct);

            if (imageObject is null)
            {
                return false;
            }

            var normalizedKey = imageObject.ObjectKey.Replace("/", Path.DirectorySeparatorChar.ToString());
            var filePath = Path.Combine(settings.UploadFolder, normalizedKey);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return await imageRepository.DeleteImageByObjectKeyAsync(objectKey, ct);

        }
        catch (Exception ex)
        {
            throw new FileNotFoundException("The Image File does not exists", ex);
        }
    }

}
