using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models.WorkingShift
{
    public class WorkingDay
    {
        public int Id { get; set; }
        public MyIdentityUser User { get; set; }
        public DateTime DateTime { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int NominalWorkingHours { get; set; }
        public int ActualWorkingHours { get; set; }
        public int HoursOverPaid50 { get; set; }
        public int HoursOverPaid100 { get; set; }
    }
}
