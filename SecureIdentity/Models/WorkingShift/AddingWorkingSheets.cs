using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models.WorkingShift
{
    public class AddingWorkingSheets
    {
        public WorkingDay sheet;

        /*
        public AddingWorkingSheets(WorkingDay workingDay)
        {
            sheet = workingDay;
        }
        */

        public WorkingDay AddingTable(MyIdentityUser user, int m, int d)
        {

                sheet = new WorkingDay();
                var date = new DateTime(DateTime.Now.Year, m, d);
                
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    sheet.Day = date.Day;
                    sheet.User = user;
                    sheet.Year = DateTime.Now.Year;
                    sheet.Month = m;
                    sheet.NominalWorkingHours = 8;
                    sheet.ActualWorkingHours = 8;
                    sheet.HoursOverPaid50 = 0;
                    sheet.HoursOverPaid100 = 0;
                    sheet.DateTime = new DateTime(DateTime.Now.Year, m, d);
                }
                else
                {
                    sheet.Day = date.Day;
                    sheet.Year = DateTime.Now.Year;
                    sheet.Month = m;
                    sheet.User = user;
                    sheet.DateTime = new DateTime(DateTime.Now.Year, m, d);

                }
            
            return sheet;
        }
    }
}
