using System.Security.Claims;
using Wait.Domain.Entities;
using Wait.Features.Users;

namespace Wait.Abstract;

public interface ITokenProvider
{
    /// <summary>
    /// Generates a signed JWT token containing user-specific claims using application configuration settings.
    /// </summary>
    /// <param name="users">The user information to encode in the token, including ID, username, and email verification status.</param>
    /// <returns>A string representing the generated JWT.</returns>
    string GenerateToken(Users users);

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
    string GenerateRefreshToken();

    /// <summary>
    /// Extracts and validates the <see cref="ClaimsPrincipal"/> from a given JWT access token.
    /// </summary>
    /// <param name="accessToken">
    /// The JWT access token from which to extract the claims principal.
    /// </param>
    /// <returns>
    /// A <see cref="ClaimsPrincipal"/> object representing the authenticated user's identity and claims.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method validates the provided JWT using issuer, audience, and signing key checks. 
    /// The <c>ValidateLifetime</c> parameter is intentionally set to <c>false</c> to allow extraction of claims 
    /// from expired tokens. This is particularly useful in refresh token flows, where you may need to 
    /// read the user information from an expired access token in order to issue a new one without 
    /// requiring the user to log in again.
    /// </para>
    /// <para>
    /// Typical usage in a refresh token scenario:
    /// <list type="number">
    ///   <item>Receive the expired access token and valid refresh token from the client.</item>
    ///   <item>Call <c>GetClaimsPrincipalFromToken</c> to extract the user's claims from the expired access token.</item>
    ///   <item>Validate the refresh token against stored data.</item>
    ///   <item>Generate a new access token (and optionally a new refresh token) using the retrieved claims.</item>
    /// </list>
    /// </para>
    /// <para>
    /// If the token is invalid or cannot be parsed, a <see cref="SecurityTokenException"/> will be thrown.
    /// </para>
    /// </remarks>
    /// <exception cref="SecurityTokenException">
    /// Thrown when the token is invalid or cannot be validated.
    /// </exception>
    ClaimsPrincipal GetClaimsPrincipalFromToken(string accessToken);
}