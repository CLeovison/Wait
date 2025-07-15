namespace Wait.Contracts.Data;

public class ProductDto
{
    public int ProductId { get; init; }
    public required string ProductName { get; set; }
    public required string ProductSize { get; set; }
    public required string Category { get; set; }
    public int Quantity { get; init; }

    public DateOnly CreatedAt { get; init; }

}