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
    Task<Stream> GetImageByIdAsync(string id, string fileName, int? width, CancellationToken ct);
    Task<ImageOperationResult> DeleteImageAsync(string id, CancellationToken ct);
}
