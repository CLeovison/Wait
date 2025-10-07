
namespace Wait.Contracts.Request.ProductRequest;


public sealed class CreateProductRequest
{
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public required string Size { get; init; }
    public required string Color { get; init; }
    public IFormFile? Image { get; init; }

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int Quantity { get; init; }
    public DateOnly CreatedAt { get; init; }
}