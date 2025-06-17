namespace Wait.Contracts.Request.UserRequest;


public sealed class FilterUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? CreatedAt { get; set; }
}