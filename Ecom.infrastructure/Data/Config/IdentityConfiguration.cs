using Ecom.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Data.Config
{
    public class IdentityConfiguration
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser
                {
                    displayName = "Ahmad47",
                    Email = "ahmad222jarad@gmail.com",
                    UserName = "Ahmad-Jarad",
                    Address = new Address
                    {
                        FirstName="Ahmad",
                        LastName="Jarad",
                        zipcode="123",
                        street="test-street",
                        city="Giza"
                        ,state="haram"
                    }
                };
                await userManager.CreateAsync(User,"Ahmad1111.");
            }
        }
    }
}
