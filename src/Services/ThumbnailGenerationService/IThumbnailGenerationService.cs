namespace Wait.Services.ThumbnailGenerationService;

public interface IThumbnailGenerationService
{
    Task ProcessThumbnailAsync(CancellationToken ct);
    Task ExecuteAsync(CancellationToken ct);
}
