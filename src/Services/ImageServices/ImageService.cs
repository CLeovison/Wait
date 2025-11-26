using System.Security.Claims;
using Microsoft.Extensions.Options;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Wait.Infrastructure.Common;
using Wait.Infrastructure.Configuration;
using Wait.Infrastructure.Repositories;
using Wait.Services.FileServices;

namespace Wait.Services.ImageServices;


public sealed class ImageService(IOptions<UploadDirectoryOptions> options, IHttpContextAccessor httpContext, IImageRepository imageRepository) : IImageService
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

    public async Task<Stream> GetImageByIdAsync(string id, string fileName, int? width, CancellationToken ct)
    {
        try
        {
            var image = await imageRepository.GetImageByIdAsync(id, ct);

            if (image is null)
            {
                throw new FileNotFoundException("The file cannot be found");
            }

            var filePath = Path.Combine(settings.UploadFolder, image.ObjectKey);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File does not exist on disk");

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }
        catch (Exception ex)
        {
            throw new IOException("Error : The Image cannot be retrived", ex);
        }
    }
    public async Task<ImageResult> UploadImageAsync(IFormFile file, CancellationToken ct)
    {
        if (!IsValidImage(file))
        {
            throw new ArgumentException("Invalid Image Filetype");
        }

        try
        {
            var imageId = Guid.NewGuid().ToString();
            var folderPath = Path.Combine(settings.UploadFolder, "images", imageId);
            var fileName = $"{imageId}{Path.Combine(file.FileName)}";

            // TODO: Enqueue thumbnail generation job when background processing is introduced.
            var originalPath = await SaveOriginalImageAsync(file, folderPath, fileName, ct);

            var context = httpContext.HttpContext;
            var relativePath = Path.Combine("uploads", "images", imageId, fileName).Replace("\\", "/");
            var url = $"{context?.Request.Scheme}://{context?.Request.Host}/{relativePath}";

            var userId = httpContext?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = httpContext?.HttpContext?.User?.Identity?.Name;

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID not found in claims");
            }

            var imageResult = new ImageResult
            {
                ImageId = Guid.Parse(imageId),
                ObjectKey = originalPath,
                StorageUrl = url,
                DateUploaded = DateTime.UtcNow,
                OriginalFileName = file.FileName,
                MimeType = file.ContentType,
                FileExtension = Path.GetExtension(file.FileName),
                UserId = Guid.Parse(userId),
            };
            return await imageRepository.UploadImageAsync(imageResult, ct);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("The Image cannot be uploaded", ex);
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
