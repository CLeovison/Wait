namespace Wait.Infrastructure.Common;


public sealed class ImageOperationResult
{
    public string ImageId { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
}