public static class SortDefaults
{
    public static string GetDefaultSortField(string entityType) => entityType switch
    {
        "User" => "FirstName",
        "Product" => "Name",
        "Order" => "OrderDate",
        "Category" => "CategoryName",
        _ => "Id"
    };
}
