using Microsoft.EntityFrameworkCore;
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
    public async Task<ImageResult?> GetImageByIdAsync(string id, CancellationToken ct)
    {
        return await dbContext.Image.FindAsync(id, ct);
    }

    public async Task<ImageResult?> GetImageByObjectKeyAsync(string objectKey, CancellationToken ct)
    {
        return await dbContext.Image.FirstOrDefaultAsync(x => x.ObjectKey == objectKey);
    }

}