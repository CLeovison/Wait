using System.Text.Json.Serialization;
using Wait.Domain.Entities;

namespace Wait.Contracts.Request.ProductRequest;


public sealed class CreateProductRequest
{
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public required string Size { get; init; }

    [JsonIgnore]
    public Category? Category { get; set; }
    public int Quantity { get; init; }
    public DateOnly CreatedAt { get; init; }
}