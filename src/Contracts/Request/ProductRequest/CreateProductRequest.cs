
namespace Wait.Contracts.Request.ProductRequest;

//Key considerations : I can just create a product and later on add an image using a gallery 
// which the admin can just get
public sealed class CreateProductRequest
{
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public required string Size { get; init; }
    public required string Color { get; init; }
    public List<string> ImageUrl { get; set; } = new();
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int Quantity { get; init; }

}