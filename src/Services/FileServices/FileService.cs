namespace Wait.Services.FileServices;


public sealed class FileService(IWebHostEnvironment environment, IConfiguration configuration) : IFileService
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

        var fileName = $"{Guid.NewGuid()}{extensions}";
        var fileNamePath = Path.Combine(path, fileName);
        using var stream = new FileStream(fileNamePath, FileMode.Create);
        await imageFile.CopyToAsync(stream);
        return fileName;

    }

    public async Task<bool> DeleteFileAsync(string imageFileExtension)
    {
        if (string.IsNullOrWhiteSpace(imageFileExtension))
        {
            throw new ArgumentNullException(nameof(imageFileExtension));
        }

        var contentPath = environment.ContentRootPath;
        var path = Path.Combine(contentPath, $"Uploads", imageFileExtension);

        if (!File.Exists(path))
        {
            throw new ArgumentNullException("The File didn't exist");
        }

        File.Delete(path);
    }
}