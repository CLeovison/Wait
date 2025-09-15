namespace Wait.Contracts.Request.CategoriesRequest;

public sealed class FilterCategoriesRequest
{
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryDescription { get; set; } = string.Empty;
}