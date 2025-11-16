using Wait.Infrastructure.Common;

namespace Wait.Infrastructure.Repositories;

public interface IImageRepository
{
    Task<ImageResult> UploadImageAsync(ImageResult imageResult, CancellationToken ct);
}