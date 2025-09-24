namespace Wait.Contracts.Request.ProductRequest;

public sealed class FilterProductRequest
{
    public string? ProductName { get; set; }
    public string? Size { get; set; } = string.Empty;
    public string? Color { get; set; } = string.Empty;
}