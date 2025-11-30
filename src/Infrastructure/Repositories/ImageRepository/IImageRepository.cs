using Wait.Infrastructure.Common;

namespace Wait.Infrastructure.Repositories;

public interface IImageRepository
{
    Task<ImageResult> UploadImageAsync(ImageResult imageResult, CancellationToken ct);
    Task<ImageResult?> GetImageByIdAsync(string id, CancellationToken ct);
    Task<ImageResult?> GetImageByObjectKeyAsync(string objectKey, CancellationToken ct);
    Task<bool> DeleteImageByObjectKeyAsync(string objectKey, CancellationToken ct);
}