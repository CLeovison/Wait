using System.Collections.Concurrent;
using System.Threading.Channels;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using Wait.Infrastructure.Common;
using Wait.Services.FileServices;

namespace Wait.Services.ThumbnailGenerationService;

public sealed class ThumbnailGenerationService(

Channel<ThumbnailGenerationJob> channel,
ConcurrentDictionary<string, ThumbnailGenerationStatus> status,
IImageService imageService) : IThumbnailGenerationService
{
    public async Task ExecuteAsync(CancellationToken ct)
    {
        await foreach (var job in channel.Reader.ReadAllAsync(ct))
        {
            try
            {
                await ProcessThumbnailAsync(job, ct);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                throw new OperationCanceledException("Processing", ex);
            }


        }
    }

    private async Task ProcessThumbnailAsync(ThumbnailGenerationJob job, CancellationToken ct)
    {
        status[job.id] = ThumbnailGenerationStatus.Processing;

        try
        {
            await imageService.GenerateThumbnailAsync(job.originalFilePath, job.folderPath, job.id);
            status[job.id] = ThumbnailGenerationStatus.Completed;
        }
        catch (Exception ex)
        {
            status[job.id] = ThumbnailGenerationStatus.Failed;
            throw new DirectoryNotFoundException("The Thumbnail Cannot be Generated", ex);
        }
    }

}