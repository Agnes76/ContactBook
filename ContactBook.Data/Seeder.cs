using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Models;
using Microsoft.AspNetCore.Identity;

namespace ContactBook.Data
{
    public class Seeder
    {
        
        public static async Task Seed(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager, ContactContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Regular" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role }) ;
                }

                List<AppUser> users = new List<AppUser>
                {
                    new AppUser
                    {
                        FirstName = "Esther",
                        LastName = "Ugochukwu",
                        Email = "esther@gmail.com",
                        UserName = "esty",
                        PhoneNumber = "09056789667"
                    },
                    new AppUser
                    {
                         FirstName = "Amarachi",
                        LastName = "Amuchionu",
                        Email = "amara@gmail.com",
                        UserName = "amy",
                        PhoneNumber = "09056789667"
                    }
                };

                foreach ( var user in users)
                {
                    await userManager.CreateAsync(user, "Agnes@5");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "Regular");
                    }
                }
            }
        }
    }
}
