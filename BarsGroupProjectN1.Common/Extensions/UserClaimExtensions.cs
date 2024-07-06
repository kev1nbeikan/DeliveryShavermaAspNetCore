using System.Security.Claims;
using BarsGroupProjectN1.Core.Models;
using BarsGroupProjectN1.Core.Contracts;

namespace BarsGroupProjectN1.Core.Extensions
{
    public static class UserClaimsExtensions
    {
        public static Guid UserId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst(UserClaimsStrings.UserId)!.Value ?? string.Empty);
        }

        public static string Role(this ClaimsPrincipal user)
        {
            return user.FindFirst(UserClaimsStrings.Role)!.Value ?? string.Empty;
        }

        public static Guid IdOfRequest { get; set; }
        public static string RoleOfRequest { get; set; }
    }
}