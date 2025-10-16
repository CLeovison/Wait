using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Wait.Infrastructure.Common;
using Wait.Infrastructure.Configuration;
using Wait.Services.FileServices;

namespace Wait.Services.ImageServices;


public sealed class ImageService(IOptions<UploadDirectoryOptions> options) : IImageService
{
    private readonly UploadDirectoryOptions settings = options.Value;

    public bool IsValidImage(IFormFile file)
    {
        if (file.Length == 0)
        {
            return false;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        return settings.AllowedExtensions.Contains(extension) && settings.AllowedMimeTypes.Contains(file.ContentType);
    }

    public async Task<string> SaveOriginalImageAsync(IFormFile file, string folderPath, string folderName)
    {
        if (!IsValidImage(file))
        {
            throw new ArgumentException("Invalid image file", nameof(file));
        }

        var originalFilePath = Path.Combine(folderPath, folderName);

        Directory.CreateDirectory(originalFilePath);

        using var stream = new FileStream(originalFilePath, FileMode.Create);

        await file.CopyToAsync(stream);

        return originalFilePath;
    }


    public async Task<IEnumerable<string>> GenerateThumbnailAsync(
        string originalFilePath,          // Path of the original image
        string folderPath,                // Where to save thumbnails
        string fileNameWithoutExtension,  // Base name for thumbnails
        int[]? widths = null)             // Optional custom widths (defaults to ThumbnailsWidth)
    {

        var thumbnailPaths = new List<string>();

        var extension = Path.GetExtension(originalFilePath);

        widths ??= options.Value.ThumbnailsWidth;


        using var image = await Image.LoadAsync(originalFilePath);

        foreach (var width in widths)
        {
            var thumbnailFileName = $"{fileNameWithoutExtension}_w{width}{extension}";

            var thumbnailPath = Path.Combine(folderPath, thumbnailFileName);

            var resizedImage = image.Clone(x => x.Resize(width, 0));

            await resizedImage.SaveAsync(thumbnailPath);

            thumbnailPaths.Add(thumbnailPath);
        }

        return thumbnailPaths;
    }

    public async Task<ImageUploadResult> UploadImageAsync(IFormFile file, CancellationToken ct)
    {
        if (!IsValidImage(file))
        {
            throw new ArgumentException("Invalid Image Filetype");
        }

        var imageId = Guid.NewGuid().ToString();
        var folderPath = Path.Combine(settings.UploadFolder, "images", imageId);
        var fileName = $"{imageId}{Path.Combine(file.FileName)}";

        var originalPath = await SaveOriginalImageAsync(file, folderPath, fileName);

        var job = new ThumbnailGenerationJob(imageId, originalPath, folderPath);

        return new ImageUploadResult
        {
            ImageId = imageId,
            ImagePath = originalPath,
            UploadedAt = DateTime.UtcNow,
            ImageName = file.FileName
        };
    }

}