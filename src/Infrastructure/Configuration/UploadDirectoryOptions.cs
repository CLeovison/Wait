namespace Wait.Infrastructure.Configuration;


public class UploadDirectoryOptions
{
    public string UploadFolder { get; set; } = string.Empty;
    public List<string> AllowedExtensions { get; set; } = new();
    public List<string> AllowedMimeTypes { get; set; } = new();
    public int[] ThumbnailsWidth { get; set; } = Array.Empty<int>();
    public long ImageFileSize { get; set; }
}