using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Wait.Services.FileServices;

namespace Wait.Services.ImageServices;


public sealed class ImageService(IConfiguration configuration) : IImageService
{
    private static readonly int[] ThumbnailsWidth = [32, 64, 128, 256, 512, 1024];

    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];

    private static readonly string[] AllowedMimeTypes = ["image/jpeg", "image/png", "image/gif"];

    private readonly string uploadDirectory = configuration["UploadDirectory : UploadFolder"]!;

    public bool IsValidImage(IFormFile file)
    {
        if (file.Length == 0)
        {
            return false;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        return AllowedExtensions.Contains(extension) && AllowedMimeTypes.Contains(file.ContentType);
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

        widths ??= ThumbnailsWidth;


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

    public async Task<string> UploadImageAsync(IFormFile file, CancellationToken ct)
    {
        if (IsValidImage(file))
        {
            throw new ArgumentException("Invalid Image File");
        }

        var imageId = Guid.NewGuid().ToString();
        var folderPath = Path.Combine(uploadDirectory, "images", imageId);
        var fileName = $"{imageId}{Path.Combine(file.FileName)}";

        var originalPath = await SaveOriginalImageAsync(file, folderPath, fileName);
    }

}