using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Wait.Abstract;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Authentication;

public sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{

    public string GenerateToken(Users users)
    {
        string? secretKey = configuration["Jwt:SecretKey"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, users.UserId.ToString()),
                new Claim(ClaimTypes.Name, users.Username),
                new Claim(ClaimTypes.Email, users.IsVerifiedEmail.ToString())
            ]),

            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:Expirations")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JwtSecurityTokenHandler();

        return handler.CreateEncodedJwt(tokenDescriptor);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    public ClaimsPrincipal GetClaimsPrincipalFromToken(string accessToken)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
            NameClaimType = ClaimTypes.NameIdentifier,

        };

        var handler = new JwtSecurityTokenHandler();
        try
        {
            var principal = handler.ValidateToken(accessToken, tokenValidation, out _);
            return principal;
        }
        catch (Exception ex)
        {
            throw new SecurityTokenException("Invalid token", ex);
        }
        ;
    }
    
    
}