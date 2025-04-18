using Microsoft.AspNetCore.Identity;
using Wait.Entities;

namespace Wait.Infrastracture;

public sealed class PasswordHasher : IPasswordHasher<Users>
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;
    

    public string HashPassword(Users user, string password)
    {
        // Example implementation using a simple hash
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public PasswordVerificationResult VerifyHashedPassword(Users user, string hashedPassword, string providedPassword)
    {
        // Example implementation for verifying the hashed password
        var providedPasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(providedPassword));
        return hashedPassword == providedPasswordHash ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}