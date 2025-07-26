namespace Wait.Contracts.Response;


public sealed class LoginResponse(string accessToken, string refreshToken)
{


    public string AccessToken { get; set; } = accessToken;
    public string RefreshToken { get; set; } = refreshToken;
}