using Wait.Domain.Entities;

namespace Wait.Abstract;

public interface ITokenProvider
{
    string GenerateToken(Users users);
    string GenerateRefreshToken();
}