using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Wait.Entities;

namespace Wait.Infrastracture;

public sealed class PasswordHasher : IPasswordHasher<Users>
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public string HashPassword(Users user, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public PasswordVerificationResult VerifyHashedPassword(Users user, string hashedPassword, string providedPassword)
    {
      
        var providedPasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(providedPassword));
        return hashedPassword == providedPasswordHash ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}