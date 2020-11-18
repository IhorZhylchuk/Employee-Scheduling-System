using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Models;
using SecureIdentity.Models.WorkingShift;

namespace SecureIdentity.Controllers
{
    public class WorkingShiftController : Controller
    {
        private readonly MyIdentityDbContext db;

        public WorkingShiftController(MyIdentityDbContext context)
        {
            db = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> WorkingTime(string id)
        {
            if (id != null)
            {
                var user = await db.Users.FirstOrDefaultAsync(i => i.Id == id);

                var monthNumber = DateTime.Now.Month;

                var daysSheet = await db.WorkingDays.Where(m => m.Month == monthNumber).Where(u => u.User == user).OrderBy(d => d.Day).ToListAsync();

                List<WorkingDay> workingDays = new List<WorkingDay>();
                workingDays = daysSheet;

                var model = new MonthWorkingModel();
                model.WorkingDays = daysSheet;


                ViewBag.User = user.Id;

                var day = new Days();
                int nOfWD = day.GetNumberOfWorkingDays(db, user);
                int actualWH = day.GetNumberOfActualWorkingHours(db, user, nOfWD * 8);
                int hOverpaid100 = day.GetNumberOfOverpaidHours100(db, user);
                int hOverpaid50 = day.GetNumberOfOverpaidHours50(db, user);

                ViewBag.NominalWD = nOfWD;
                ViewBag.NominalWH = nOfWD * 8;
                ViewBag.ActualWH = actualWH;
                ViewBag.HOverpaid50 = hOverpaid50;
                ViewBag.HOverpaid100 = hOverpaid100;

                //var monthes = new List<string> {"January","February", "March", "April", "May"};
                //ViewBag.Monthes = monthes;

                return View(model);
            }
            return NotFound();

        }


        [HttpPost]
        public async Task<IActionResult> WorkingTime(MonthWorkingModel model)
        {
            var user = model.WorkingDays.Select(u => u.User).FirstOrDefault();
            var listOfDays = await db.WorkingDays.Where(u => u.User == user).OrderBy(d => d.Day).ToListAsync();

            db.WorkingDays.RemoveRange(listOfDays);
            db.SaveChanges();

            try
            {
                db.WorkingDays.UpdateRange(model.WorkingDays.OrderBy(d => d.Day));
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Account");
            }
            catch (DbUpdateException ex)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return NotFound();
        }

    

        [HttpGet]
        public async Task<IActionResult> WorkTime(string id, int number)
        {
            if (id != null)
            {
                var user = await db.Users.FirstOrDefaultAsync(i => i.Id == id);

                var month = DateTime.Now.Month + number;

                var daysSheet = await db.WorkingDays.Where(m => m.Month == month).Where(u => u.User == user).OrderBy(d => d.Day).ToListAsync();

                List<WorkingDay> workingDays = new List<WorkingDay>();
                workingDays = daysSheet;

                var model = new MonthWorkingModel();
                model.WorkingDays = daysSheet;


                ViewBag.User = user.Id;

                var day = new Days();
                int nOfWD = day.GetNumberOfWorkingDays(db, user);
                int actualWH = day.GetNumberOfActualWorkingHours(db, user, nOfWD * 8);
                int hOverpaid100 = day.GetNumberOfOverpaidHours100(db, user);
                int hOverpaid50 = day.GetNumberOfOverpaidHours50(db, user);

                ViewBag.NominalWD = nOfWD;
                ViewBag.NominalWH = nOfWD * 8;
                ViewBag.ActualWH = actualWH;
                ViewBag.HOverpaid50 = hOverpaid50;
                ViewBag.HOverpaid100 = hOverpaid100;

                //var monthes = new List<string> {"January","February", "March", "April", "May"};
                //ViewBag.Monthes = monthes;

                return View(model);
            }
            return NotFound();

        }


        [HttpPost]
        public async Task<IActionResult> WorkTime(MonthWorkingModel model)
        {
            var user = model.WorkingDays.Select(u => u.User).FirstOrDefault();
            var listOfDays = await db.WorkingDays.Where(u => u.User == user).OrderBy(d => d.Day).ToListAsync();

            db.WorkingDays.RemoveRange(listOfDays);
            db.SaveChanges();

            try
            {
                db.WorkingDays.UpdateRange(model.WorkingDays.OrderBy(d => d.Day));
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Account");
            }
            catch (DbUpdateException ex)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return NotFound();
        }


    }
}