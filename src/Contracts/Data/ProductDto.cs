
using Wait.Domain.Common;

namespace Wait.Contracts.Data;

public class ProductDto : AuditableEntity
{
    public Guid ProductId { get; init; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public double Price { get; set; }
    public Guid CategoryId { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Quantity { get; set; }
}