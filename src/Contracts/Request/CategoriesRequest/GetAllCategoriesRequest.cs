using Wait.Contracts.Data;

namespace Wait.Contracts.Request.CategoriesRequest;

public sealed class GetAllCategoriesRequest
{
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryDescription { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public ICollection<ProductDto>? Products { get; set; }
}