using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiFinal.Extensions
{
    public static class UserMangerExtension
    {
        public static async Task<AppUser> FindByUserbyClaimsPrincipleWithAddressAsync(this UserManager<AppUser> input,
            ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync( x=> x.Email  == email);
        }

        public static async Task<AppUser> FindEmailFromClaimsPrinciple(
            this UserManager<AppUser> input  , ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            return await input.Users.SingleOrDefaultAsync(x => x.Email ==email);
        }
    }
}
