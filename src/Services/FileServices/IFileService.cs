namespace Wait.Services.FileServices;


public interface IFileService
{
    Task<string> UploadImageAsync(IFormFile imageFile, string[] imageFileExtension);
    Task DeleteImageAsync(string imageFile);
}