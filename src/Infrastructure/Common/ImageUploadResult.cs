namespace Wait.Infrastructure.Common;


public sealed class ImageResult
{
    public string ImageId { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;

    public string ImageName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string UploadedBy { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}