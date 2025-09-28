
using Wait.Domain.Common;


namespace Wait.Contracts.Data;

public class ProductDtoUpload : AuditableEntity
{

    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public IFormFile? Image { get; set; }

    public string ImageName { get; set; } = string.Empty;



}