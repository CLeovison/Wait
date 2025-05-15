namespace Wait.Entities;


public class Product
{
    public Guid ProductId { get; init; }
    public required string ProductName { get; set; }
    public required string Category { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public int Quantity { get; set; }
    public required string Size { get; set; }
    public required string Image { get; set; }
    public bool IsSoftDelete { get; init; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly UpdatedAt { get; set; }
}