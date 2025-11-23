namespace Wait.Infrastructure.Common;


public sealed class ImageResult
{
    public Guid ImageId { get; set; } 
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public string ObjectKey { get; set; } = string.Empty;
    public string StorageUrl { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public DateTime DateUploaded { get; set; }
    public DateTime DateModified { get; set; }
}