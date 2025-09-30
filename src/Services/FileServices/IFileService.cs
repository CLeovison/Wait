namespace Wait.Services.FileServices;


public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile imageFile, string[] imageFileExtension);
    Task DeleteFileAsync(string imageFileExtension);
}