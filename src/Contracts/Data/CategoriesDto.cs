
using Wait.Domain.Common;
using Wait.Entities;

namespace Wait.Domain.Entities;

public sealed class CategoryDto : AuditableEntity
{
    public Guid CategoryId { get; set; } = Guid.CreateVersion7();
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryDescription { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public  ICollection<Product>? Products { get; set; } 
}