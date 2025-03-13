using Wait.ValueObjects.Product;

namespace Wait.Entities;


public class Product
{
    public int ProductId { get; init; }
    public required string ProductName { get; init; }
    public required string ProductType { get; init; }
    public required string Description { get; init; }
    public required Price Price { get; init; }
    public int Quantity { get; init; }
    public bool IsSoftDelete { get; init; }
    public DateOnly CreatedAt { get; init; }
    public DateOnly UpdatedAt { get; init; }
}