using System.Security.Claims;

namespace BlazorDictionary.Api.WebApi.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal User)
        {
            return new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
