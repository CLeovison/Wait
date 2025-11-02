using Wait.Infrastructure.Common;

namespace Wait.Services.ThumbnailGenerationService;

public interface IThumbnailGenerationService
{
    Task ExecuteAsync(CancellationToken ct);
    Task ProcessThumbnailAsync(ThumbnailGenerationJob job, CancellationToken ct);
    Task GetThumbnailStatusAsync(CancellationToken ct);
}
