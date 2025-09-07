namespace Wait.Contracts.Request.CategoriesRequest;

public sealed class CreateCategoriesRequest
{
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryDescription { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}