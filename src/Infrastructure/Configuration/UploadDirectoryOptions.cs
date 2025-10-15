namespace Wait.Infrastructure.Configuration;


public sealed class UploadDirectoryOptions
{
    public string UploadFolder { get; set; } = string.Empty;
    public string[] AllowedExtensions { get; set; } = [];
    public string[] AllowedMimeTypes { get; set; } = [];
    public int[] ThumbnailsWidth { get; set; } = [];

    public long ImageFileSize { get; set; }

}