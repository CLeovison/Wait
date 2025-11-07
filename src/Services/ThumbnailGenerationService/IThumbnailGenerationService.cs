using Wait.Infrastructure.Common;

namespace Wait.Services.ThumbnailGenerationService;

public interface IThumbnailGenerationService
{
    Task ExecuteAsync(CancellationToken ct);
    Task ProcessThumbnailAsync(ThumbnailGenerationJob job);
    // Task GetThumbnailStatusAsync(string id);
   
    // Task DeleteThumbnailAsync(string folderPath, CancellationToken ct);
}
