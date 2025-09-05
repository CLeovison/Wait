namespace Wait.Contracts.Request.ProductRequest;


public sealed class CreateProductRequest
{
    public required string ProductName { get; init; }
    public double Price { get; init; }
    public required string Description { get; init; }
    public required string Size { get; init; }
    public required string Category { get; init; }
    public int Quantity { get; init; }
    public DateOnly CreatedAt { get; init; }
}