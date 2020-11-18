using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecureIdentity.Models;

namespace SecureIdentity.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<MyIdentityUser> userManager;

        public HomeController(UserManager<MyIdentityUser> manager)
        {
            this.userManager = manager;
        }
        [Authorize]
        public IActionResult Index()
        {
            
                MyIdentityUser user = userManager.GetUserAsync
                          (HttpContext.User).Result;

            //MyIdentityUser user2 = userManager.GetUserNameAsync(HttpContext.User.Identity.Name.ToString()).Result;

            try
            {
               // ViewBag.User = user.FullName;

            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
                /*
                if (userManager.IsInRoleAsync(user, "NormalUser").Result)
                {
                    ViewBag.RoleMessage = "You are a NormalUser.";
                }
                */


            return View();
        }
    }
}
