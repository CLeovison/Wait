namespace Wait.Contracts.Request.Common;

public sealed class ImageRequest
{
    public required string ImageId { get; init; }
    public required string StatusUrl { get; init; }
    public required ThumbnailGenerationStatus Status { get; set; }
}