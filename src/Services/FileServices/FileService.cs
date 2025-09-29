namespace Wait.Services.FileServices;


public sealed class FileService(IWebHostEnvironment environment) : IFileService
{
    public async Task<string> SaveFileAsync(IFormFile imageFile, string[] imageFileExtension)
    {
        if (imageFile is null)
        {
            throw new ArgumentNullException(nameof(imageFile));
        }

        var contentPath = environment.ContentRootPath;
        var path = Path.Combine(contentPath, "Uploads");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path); 
        }

        var extensions = Path.GetExtension(imageFile.FileName);

        if (!imageFileExtension.Contains(extensions))
        {
            throw new ArgumentException($"Only {string.Join(",", imageFileExtension)} are allowed");
        }

        var fileName = $"{Guid.NewGuid().ToString()}{extensions}";
        var fileNamePath = Path.Combine(path, fileName);
        using var stream = new FileStream(fileNamePath, FileMode.Create);
        await imageFile.CopyToAsync(stream);
        return fileName;

    }

    public async Task<bool> DeleteFileAsync(string imageFileExtension)
    {
        return imageFileExtension is not null;
    }
}