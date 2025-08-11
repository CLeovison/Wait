

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Wait.Abstract;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Authentication;

public sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    
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
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    public async Task<ClaimsPrincipal> GetClaimsPrincipalFromToken(string accessToken)
    {
        var tokenValidation = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
        };

        var handler = new JsonWebTokenHandler();

        var result = await handler.ValidateTokenAsync(accessToken, tokenValidation);

        if (!result.IsValid || result.ClaimsIdentity == null)
        {
            throw new SecurityTokenException("Invalid token");
        }
        var principal = new ClaimsPrincipal(result.ClaimsIdentity);

        return principal;
    }
}