using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Infrastructure.Common;

namespace Wait.Infrastructure.Repositories;

public sealed class ImageRepository(AppDbContext dbContext) : IImageRepository
{
    public async Task<ImageResult> UploadImageAsync(ImageResult imageResult, CancellationToken ct)
    {
        await dbContext.Image.AddAsync(imageResult, ct);
        await dbContext.SaveChangesAsync(ct);
        return imageResult;
    }

    public async Task<ImageResult?> GetImageByIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.Image.FindAsync([id], ct);
    }

    public async Task<IReadOnlyList<ImageResult>> GetImagesByObjectKeyAsync(string objectKey, CancellationToken ct)
    {
        return await dbContext.Image
            .Where(x => x.ObjectKey == objectKey)
            .ToListAsync(ct);
    }

    public async Task<bool> DeleteImageByObjectKeyAsync(string objectKey, CancellationToken ct)
    {
        var imageKey = await dbContext.Image.FirstOrDefaultAsync(x => x.ObjectKey == objectKey, ct);
        if (imageKey is null) return false;

        dbContext.Image.Remove(imageKey);
        await dbContext.SaveChangesAsync(ct);
        return true;
    }
}