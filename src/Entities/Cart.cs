using Wait.Domain.Entities;

namespace Wait.Entities;

public sealed class Cart
{
    public int CarId { get; set; }
    public int Quantity { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public int ProductId { get; set; }
    public Product? Products { get; set; }
}