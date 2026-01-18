using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skinet.Entities;
using System.Security.Authentication;
using System.Security.Claims;

namespace skinet.Extension
{
    public static class ClaimPrincipleExtension
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager,ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == claimsPrincipal.GetEmail());
            if(user == null)
            {
                throw new Exception("User not found");
            }
            return  user;
            
        }
        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.Users.Include(x=>x.Address).FirstOrDefaultAsync(x => x.Email == claimsPrincipal.GetEmail());
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;

        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var email= claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            if(email== null)
            {
                throw new Exception("Email claim not found");
            }
            return email;
        }

    }
}
