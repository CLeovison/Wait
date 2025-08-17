namespace Wait.Domain.Entities;


public sealed class Category
{
    public Guid CategoryId { get; set; } = Guid.CreateVersion7();
    public string CategoryName { get; set; } = string.Empty;

}