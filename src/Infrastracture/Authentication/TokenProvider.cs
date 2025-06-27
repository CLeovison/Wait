

using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Wait.Domain.Entities;

namespace Wait.Infrastracture;

public sealed class TokenProvider(IConfiguration configuration)
{
    public string Create(Users users)
    {
        string secretKey = configuration["Jwt:SecretKey"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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
}