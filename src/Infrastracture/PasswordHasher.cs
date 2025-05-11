using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Request.UserRequest;
using Wait.Entities;


namespace Wait.Infrastracture;

public sealed class PasswordHasher : IPasswordHasher<UserDto>
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public string HashPassword(UserDto userDto, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public PasswordVerificationResult VerifyHashedPassword(UserDto user, string hashedPassword, string providedPassword)
    {

        var providedPasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(providedPassword));
        return hashedPassword == providedPasswordHash ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}