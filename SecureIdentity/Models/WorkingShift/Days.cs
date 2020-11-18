using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models.WorkingShift
{
    public class Days
    {
        /*
        public int GetNumberOfDays()
        {
            return (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
        */
        public int GetNumberOFDays(int month)
        {
            return (DateTime.DaysInMonth(DateTime.Now.Year, month));
        }
        public int GetNumberOfWorkingDays(MyIdentityDbContext context, MyIdentityUser user)
        {
            int i = 0;
            var days = context.WorkingDays.Where(u => u.User == user).ToList();

            foreach (var d in days)
            {
                var date = new DateTime(d.Year, d.Month, d.Day);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    i++;
                }
            }
            return i;
        }
        public int GetNumberOfActualWorkingHours(MyIdentityDbContext context, MyIdentityUser user, int nominalWH)
        {
            int i = 0;
            var days = context.WorkingDays.Where(u => u.User == user).ToList();

            foreach (var d in days)
            {
                i += d.HoursOverPaid100 + d.HoursOverPaid50;
            }
            return i + nominalWH;
        }
        public int GetNumberOfOverpaidHours100(MyIdentityDbContext context, MyIdentityUser user)
        {
            int i = 0;
            var days = context.WorkingDays.Where(u => u.User == user).ToList();

            foreach (var d in days)
            {
                i +=d.HoursOverPaid100;
            }
            return i;
        }
        public int GetNumberOfOverpaidHours50(MyIdentityDbContext context, MyIdentityUser user)
        {
            int i = 0;
            var days = context.WorkingDays.Where(u => u.User == user).ToList();

            foreach (var d in days)
            {
                i +=d.HoursOverPaid50;
            }
            return i;
        }
    }
}
