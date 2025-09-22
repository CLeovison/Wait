using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

using Wait.Domain.Entities;

namespace Wait.Infrastructure.Authentication;

public sealed class PasswordHasher : IPasswordHasher<User>
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public string HashPassword(User user, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(salt)}-{Convert.ToHexString(hash)}";
    }


    public PasswordVerificationResult VerifyHashedPassword(User user, string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split('-');
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        bool verifiedPassword = CryptographicOperations.FixedTimeEquals(hash, inputHash);

        return verifiedPassword ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}