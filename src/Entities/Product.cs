
using Wait.Domain.Common;

namespace Wait.Entities;

public class Product : AuditableEntity
{
    public Guid ProductId { get; init; } = Guid.CreateVersion7();
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Guid CategoryId { get; set; } = Guid.CreateVersion7();
    public Category? Category { get; set; }
}