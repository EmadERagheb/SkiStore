using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkiStore.Domain.Identity;
using System.Security.Claims;

namespace SkiStore.API.Extensions
{
    public static class UserMangerExtension
    {
        public static async Task<AppUser> FindUserByClaimPrincipleWithAddress(this UserManager<AppUser> userManager,
            ClaimsPrincipal claims)
        {
            var email = claims.FindFirstValue(ClaimTypes.Email);

            return string.IsNullOrEmpty(email) ? null : await userManager.Users.Include(q=>q.Address).FirstOrDefaultAsync(q=>q.Email==email);

        }
    }
}
