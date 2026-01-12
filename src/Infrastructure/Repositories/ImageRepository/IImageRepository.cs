using Wait.Infrastructure.Common;

namespace Wait.Infrastructure.Repositories;

public interface IImageRepository
{
   Task<ImageResult> UploadImageAsync(ImageResult imageResult, CancellationToken ct);
    Task<ImageResult?> GetImageByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<ImageResult>> GetImagesByObjectKeyAsync(string objectKey, CancellationToken ct);
    Task<bool> DeleteImageByObjectKeyAsync(string objectKey, CancellationToken ct);
}