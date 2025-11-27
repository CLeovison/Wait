using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.Options;
using Wait.Infrastructure.Common;
using Wait.Infrastructure.Configuration;
using Wait.Services.FileServices;

namespace Wait.Services.ThumbnailGenerationService;

public sealed class ThumbnailGenerationService(

Channel<ThumbnailGenerationJob> channel,
ConcurrentDictionary<string, ThumbnailGenerationStatus> status,
IImageService imageService,
IOptions<UploadDirectoryOptions> options) : IThumbnailGenerationService
{
    public async Task ExecuteAsync(CancellationToken ct)
    {
        await foreach (var job in channel.Reader.ReadAllAsync(ct))
        {
            try
            {
                await ProcessThumbnailAsync(job);
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

    public async Task ProcessThumbnailAsync(ThumbnailGenerationJob job)
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

    // public Task GetThumbnailStatusAsync(string id)
    // {

    // }

}
