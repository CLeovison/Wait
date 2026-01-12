namespace Wait.Contracts.Data;

public class ImageDto
{
    public Guid ImageId { get; set; }
    public string ObjectKey { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
}