using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureIdentity.Models.AnnualLeave;

namespace SecureIdentity.Models
{
    public static class MyIdentityDataInitializer
    {
        
        public static void SeedData(RoleManager<MyIdentityRole> roleManager, UserManager<MyIdentityUser> userManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        public static void SeedRoles(RoleManager<MyIdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                MyIdentityRole role = new MyIdentityRole();
                role.Name = "User";

                IdentityResult result = roleManager.CreateAsync(role).Result;

            }
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                MyIdentityRole role = new MyIdentityRole();
                role.Name = "Administrator";

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

            }
        }
        public static void SeedUsers(UserManager<MyIdentityUser> userManager)
        {
            
           if(userManager.FindByNameAsync("User").Result == null)
            {
                MyIdentityUser user = new MyIdentityUser();
                user.UserName = "Petro";
                user.FullName = "Petro Soroka";
                user.Email = "user@localhost.com";
                user.DateOfBirth = new DateTime(1991, 2, 28);
                user.Role = "User";


                IdentityResult result = userManager.CreateAsync(user, "Qwerty12345!").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                    
                }
                if(userManager.FindByNameAsync("Administrator").Result == null)
                {
                    MyIdentityUser user2 = new MyIdentityUser();
                    user2.UserName = "Sara";
                    user2.FullName = "Sara Konor";
                    user2.Email = "admin@localhost.com";
                    user2.DateOfBirth = new DateTime(1993, 3, 12);
                    user2.Role = "Administrator";

                    IdentityResult result2 = userManager.CreateAsync(user2, "Qwerty12345!").Result;
                    if (result2.Succeeded)
                    {
                        userManager.AddToRoleAsync(user2, "Administrator").Wait();
                        
                    }
                }
            }
        }
    }
}
