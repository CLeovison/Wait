using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Authentication;

public sealed class TokenProvider(IConfiguration configuration)
{
    /// <summary>
    /// Generates a signed JWT token containing user-specific claims using application configuration settings.
    /// </summary>
    /// <param name="users">The user information to encode in the token, including ID, username, and email verification status.</param>
    /// <returns>A string representing the generated JWT.</returns>
    public string GenerateToken(Users users)
    {
        //Retrive secretkey from the configuration and convert into bytes
        string secretKey = configuration["Jwt:SecretKey"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        //Generate signing credentials that uses SHA-256 for cryptography
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        /// <summary>
        /// Constructs a SecurityTokenDescriptor that defines the structure and metadata of the JWT token.
        /// </summary>
        /// <remarks>
        /// This descriptor includes user-specific claims, token expiration, signing credentials, issuer, and audience.
        /// These properties are essential for generating a secure and verifiable JWT token.
        /// </remarks>
        /// <example>
        /// Claims include user ID, username, and email verification status. 
        /// The token is set to expire based on a value from the configuration, and is signed using HMAC-SHA256.
        /// Issuer and audience values are pulled from the configuration settings.
        /// </example>

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, users.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.PreferredUsername, users.Username),
                new Claim(JwtRegisteredClaimNames.EmailVerified, users.IsVerifiedEmail.ToString())
            ]),

            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:Expirations")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt: Issuer"],
            Audience = configuration["Jwt: Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
    /// <summary>
    /// Generates a cryptographically secure, random 32-byte refresh token encoded in Base64 format.
    /// </summary>
    /// <returns>
    /// A unique refresh token string used to maintain session state between authentication cycles.
    /// </returns>
    /// <remarks>
    /// Refresh tokens are typically used in token-based authentication systems to obtain new access tokens 
    /// without requiring the user to re-authenticate. This implementation uses a strong random generator to 
    /// reduce predictability and enhance security against token forgery.
    /// </remarks>

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}