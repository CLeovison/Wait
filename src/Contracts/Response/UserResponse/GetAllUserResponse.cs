namespace Wait.Contracts.Response.UserResponse;


public sealed class GetAllUserResponse
{
    public IEnumerable<UserResponse> Users { get; init; } = Enumerable.Empty<UserResponse>();
}