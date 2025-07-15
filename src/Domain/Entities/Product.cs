namespace Wait.Entities;


public class Product
{
    public int ProductId { get; init; }
    public string ProductName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Size { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsSoftDelete { get; init; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly ModifiedAt { get; set; }
}