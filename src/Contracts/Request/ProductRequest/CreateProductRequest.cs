namespace Wait.Contracts.Request.ProductRequest;


public sealed class CreateProductRequest
{
    public  string? ProductName { get; set; }
    public double Price { get; set; }
    public required string Description { get; set; }
    public required string Size { get; init; }
    public Guid CategoryId { get; set; }

    public int Quantity { get; init; }
    public DateOnly CreatedAt { get; init; }
}