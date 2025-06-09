namespace Wait.Entities;


public sealed class Rating
{
    public Guid RatingId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Stars { get; set; }
    public required string Review { get; set; }
    public DateOnly DatePosted { get; set; }
}