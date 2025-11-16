using Wait.Database;
using Wait.Infrastructure.Common;

namespace Wait.Infrastructure.Repositories;


public sealed class ImageRepository(AppDbContext dbContext) : IImageRepository
{
    public async Task<ImageResult> UploadImageAsync(ImageResult imageResult, CancellationToken ct)
    {
        await dbContext.Image.AddAsync(imageResult);
        await dbContext.SaveChangesAsync();

        return imageResult;
    }
}