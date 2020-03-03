using System;
using System.Security.Claims;

namespace Common.WebApiCore.Identity
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var stringId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Guid.TryParse(stringId, out var currentUserId);

            return currentUserId;
        }
    }
}