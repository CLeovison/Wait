namespace Wait.Contracts.Data;

public class ProductDto
{
    public int ProductId { get; init; }
    public required string ProductName { get; init; }
    public required string ProductSize { get; init; }
    public int Quantity { get; init; }

    
}