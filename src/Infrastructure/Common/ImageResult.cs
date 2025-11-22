namespace Wait.Infrastructure.Common;


public sealed class ImageResult
{
    public Guid ImageId { get; set; }
    public string ObjectKey { get; set; } = string.Empty;
    public string StorageUrl { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime DateUploaded { get; set; }

}