

using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Wait.Domain.Entities;

namespace Wait.Infrastracture;

internal sealed class TokenProvider(IConfiguration configuration)
{
    public string Create(Users users)
    {
        string secretKey = configuration["Jwt:SecretKey"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {

        };

        var handler = new JsonWebTokenHandler();
    }
}