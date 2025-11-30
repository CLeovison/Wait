using Wait.Infrastructure.Common;

namespace Wait.Services.FileServices;


public interface IImageService
{
    bool IsValidImage(IFormFile file);
    Task<string> SaveOriginalImageAsync(IFormFile file, string folderPath, string folderName, CancellationToken ct);
    Task<IEnumerable<string>> GenerateThumbnailAsync(string originalPath,
        string folderPath,
        string fileNameWithoutExtensions,
        int[]? width = null);

    Task<ImageResult> UploadImageAsync(IFormFile file, CancellationToken ct);
    Task<ImageResult> GetImageObjectKeyAsync(string objectKey, CancellationToken ct);
    Task<Stream> GetImageStreamAsync(ImageResult image, int? width, CancellationToken ct);

    Task<ImageOperationResult> DeleteImageAsync(string objectKey, CancellationToken ct);
}
