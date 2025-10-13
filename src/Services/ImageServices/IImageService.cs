namespace Wait.Services.FileServices;


public interface IImageService
{
    bool IsValidImage(IFormFile file);
    Task<string> SaveOriginalImageAsync(IFormFile file, string folderPath, string folderName);
    Task<IEnumerable<string>> GenerateThumbnailAsync(string originalPath, string folderPath, string fileNameWithoutExtensions, int[]? width = null);

    Task<string> UploadImageAsync(IFormFile file, CancellationToken ct);
}