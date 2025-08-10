namespace Wait.Contracts.Request.UserRequest;

/// <summary>
/// Contains Request for the Filtering Method
/// </summary>


public sealed class FilterUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? CreatedAt { get; set; }
}