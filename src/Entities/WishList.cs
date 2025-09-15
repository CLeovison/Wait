using Wait.Domain.Entities;

namespace Wait.Entities;


public class WishList
{
    public int WishListId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Products { get; set; }
    public Guid UserId { get; set; }
    public User? Users { get; set; }  
}