using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Models;
using SecureIdentity.Models.AnnualLeave;
using SecureIdentity.Models.WorkingShift;

namespace SecureIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly SignInManager<MyIdentityUser> signInManager;
        private readonly RoleManager<MyIdentityRole> roleManager;
        private readonly MyIdentityDbContext myIdentityDb;
        public AccountController(MyIdentityDbContext context, UserManager<MyIdentityUser> manager, SignInManager<MyIdentityUser> sign, RoleManager<MyIdentityRole> role)
        {
            this.userManager = manager;
            this.signInManager = sign;
            this.roleManager = role;
            this.myIdentityDb = context;
            //this.substring = substring;
        }
        
        [Authorize(Roles = "Administrator")]

        public async  Task<IActionResult> Index(string searchString)
        {
            var users = await (from u in myIdentityDb.Users select u).ToListAsync();
            var result = new IndexViewModel
            {
                Users = users
            };
            if (String.IsNullOrEmpty(searchString))
            {
                return View(result);
            }
            result.Users = users.Where(u => u.FullName.Contains(searchString));
            return View(result);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            if(id != null)
            {
                MyIdentityUser user = await myIdentityDb.Users.FirstOrDefaultAsync(u => u.Id == id);
                if(user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);             
            var editedUser = new EditViewModel
                {
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email,
                    BirthDate = user.DateOfBirth,
                    Role = user.Role,
                };

                if (user != null)
                {
                    return View(editedUser);
                }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if(user != null)
            {
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.Role = model.Role;

                await userManager.RemoveFromRoleAsync(user, "User");
                await userManager.RemoveFromRoleAsync(user, "Administrator");

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return NotFound();

        }
       
        [Authorize(Roles = "Administrator")]
        public IActionResult Register()
        {
            var roles = roleManager.Roles.Select(n => n.Name).ToList();
           ViewBag.Role = new SelectList(roles, "Name");
            return View();

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Delete(string id)
        {
            MyIdentityUser userDb = await myIdentityDb.Users.FirstOrDefaultAsync(u => u.Id == id);
            var user = await userManager.FindByIdAsync(id);

            if(user != null)
            {
                var result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel obj)
        {
            if (ModelState.IsValid)
            {
                



                MyIdentityUser user = new MyIdentityUser();
                
                user.FullName = obj.FullName;
                user.Email = obj.Email;
                user.DateOfBirth = obj.BirthDate;
                user.Role = obj.Role;
                user.UserName = obj.UserName = new Substring().Sub(obj.BirthDate, obj.FullName);
                user.NiN = obj.NiN;
                user.AnnualLeaveDays = obj.AnnualLeaveDays;
                user.AnnualTotalDays = obj.AnnualTotalDays;
                //user.AnnualLeave.AnnualLeaveTotalDays = 24;

                AnnualLeaveCount annualLeaveCount = new AnnualLeaveCount();
                annualLeaveCount.AnnualLeaveTotalDays = obj.AnnualTotalDays;
                annualLeaveCount.AnnualLeaveDays = obj.AnnualLeaveDays;
                annualLeaveCount.MyIdentityUserId = user.Id;
                annualLeaveCount.User = user;
              

                IdentityResult result = await userManager.CreateAsync(user, user.NiN.Substring(0,8));
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, obj.Role);
                    myIdentityDb.AnnualLeaves.Add(annualLeaveCount);
                    await myIdentityDb.SaveChangesAsync();
                    ViewBag.Result = "Saved Succesfully";
                    TempData["Success"] = "Added Successfully!";
                    ModelState.Clear();
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                WorkingDay workingDay = new WorkingDay();
                AddingWorkingSheets workingSheets = new AddingWorkingSheets();
                Days days = new Days();

                for(int m = 1; m <= 12; m++)
                {
                    for (int i = 1; i <= days.GetNumberOFDays(m); i++)
                    {
                        myIdentityDb.WorkingDays.Add(workingSheets.AddingTable(user,m, i));
                    }
                }               
            }
            await myIdentityDb.SaveChangesAsync();
            return View(obj);
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel {ReturnUrl = returnUrl});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                //var user = myIdentityDb.Users.FirstOrDefault(u => u.UserName == obj.UserName && u.PasswordHash == obj.Password.GetHashCode().ToString());

                var result = await  signInManager.PasswordSignInAsync(obj.UserName, obj.Password, obj.RememberMe, false);
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(obj.ReturnUrl) && Url.IsLocalUrl(obj.ReturnUrl))
                    {
                        //return RedirectToAction("Index", "Home");
                        return Redirect("Login");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                ModelState.AddModelError("", "InvalidLogin!");
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff(string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        
        public async Task<IActionResult> AnnualLeave()
        {
            var users = new AnnualLeaveView
            {
                Users = await userManager.Users.Select(n => n.FullName).ToListAsync()
            };

            var user = await userManager.GetUserAsync(HttpContext.User);


            

            ViewBag.Users = new SelectList(users.Users, "Users");
           

            
            var queryLeave = new AnnualLeaveViewModel();
            //queryLeave.User = user;

            ViewBag.UserName = user.UserName;
            ViewBag.User = user;
            GetDays days = new GetDays();
            
            //ViewBag.Annual = days.GetDay(myIdentityDb.AnnualLeaves.Where(u => u.User == user).Select(a => a.AnnualLeaveTotalDays));

            return View(queryLeave);

        }
    

        [HttpPost]
        public async Task<IActionResult> AnnualLeave(AnnualLeaveViewModel viewModel)
        {

            var users = new AnnualLeaveView
            {
                Users = await userManager.Users.Select(n => n.FullName).ToListAsync()
            };

            var user = await userManager.GetUserAsync(HttpContext.User);

            GetDays day = new GetDays();
            int totalDays = day.GetDay(myIdentityDb.AnnualLeaves.Where(u => u.MyIdentityUserId == user.Id).Select(d => d.AnnualLeaveTotalDays));
            int leaveDays = day.GetDay(myIdentityDb.AnnualLeaves.Where(u => u.MyIdentityUserId == user.Id).Select(d => d.AnnualLeaveDays));
            //int totalDays = day.GetDay(myIdentityDb.Users.Where(id => id.Id == user.Id).Select(annual => annual.AnnualLeave.AnnualLeaveTotalDays));
            //int leaveDays = day.GetDay(myIdentityDb.Users.Where(id => id.Id == user.Id).Select(annual => annual.AnnualLeave.AnnualLeaveDays));



            ViewBag.Users = new SelectList(users.Users, "Users");

     
            var queryLeave = new AnnualLeaveViewModel();
           // queryLeave.Annual = viewModel.Annual;
            queryLeave.Comment = viewModel.Comment;
            queryLeave.FromDate = viewModel.FromDate;
            queryLeave.ToDate = viewModel.ToDate;
            queryLeave.User = user;


            // queryLeave.Id = viewModel.Id;
            //queryLeave.AnnualLeaveCount = count;

            //user.AnnualLeave = count;
            int daysCount = day.WeekDays(DateTime.ParseExact(queryLeave.FromDate, "yyyy-MM-dd", null), DateTime.ParseExact(queryLeave.ToDate, "yyyy-MM-dd", null));
            user.AnnualLeaveDays = user.AnnualLeaveDays - daysCount;


            ViewBag.Annual = user.AnnualLeaveDays;
 

            await userManager.UpdateAsync(user);
            myIdentityDb.Users.Update(user);
            myIdentityDb.AnnualLeaveViews.Update(queryLeave);
            //var result =  myIdentityDb.AnnualLeaves.Where(u => u.User.Id == user.Id);
          // myIdentityDb.AnnualLeaves.Update(count);
            //await myIdentityDb.AnnualLeaves.AddAsync(count);
            await myIdentityDb.SaveChangesAsync();
            
            return RedirectToAction("Index", "Home");
            
        }

       



    }




}