using SixLabors.ImageSharp;
using Wait.Services.FileServices;

namespace Wait.Services.ImageServices;


public sealed class ImageService : IImageService
{
    private static readonly int[] ThumbnailsWidth = [32, 64, 128, 256, 512, 1024];
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];
    private static readonly string[] AllowedMimeTypes = ["image/jpeg", "image/png", "image/gif"];
    public bool IsValidImage(IFormFile file)
    {
        //Will check if  there is an valid image
        if (file.Length == 0)
        {
            return false;
        }
        // Will also check if there is an existing path for the image.
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

    public async Task<IEnumerable<string>> GenerateThumbnailAsync(string originalFilePath, string folderPath, string fileNameWithoutExtension, int[]? widths = null)
    {
        var thumbnailPaths = new List<string>();
        var extension = Path.GetExtension(originalFilePath);

        widths ??= ThumbnailsWidth;

        using var image = await Image.LoadAsync(originalFilePath);
    }
}