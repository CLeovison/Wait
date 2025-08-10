namespace Wait.Entities;


public class Product
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string ProductSize { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsDeleted { get; init; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly ModifiedAt { get; set; }
}