namespace Ultima.Extensions.Security;

using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Gets a value that indicates whether the user has been authenticated.
    /// </summary>
    /// <param name="principal">
    /// The user to check for authentication.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the user was authenticated; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method is a shorthand of:
    /// <code>
    /// if (principal.Identity is { } identity &amp;&amp; identity.IsAuthenticated)
    /// {
    /// }
    /// </code>
    /// </remarks>
    public static bool IsAuthenticated(this ClaimsPrincipal principal) => principal.Identity is { } identity && identity.IsAuthenticated;
}
