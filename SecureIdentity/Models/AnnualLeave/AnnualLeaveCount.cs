using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models.AnnualLeave
{
    public class AnnualLeaveCount
    {
        public int Id { get; set; }
        public MyIdentityUser User { get; set; }
        public int AnnualLeaveTotalDays { get; set; }
        public int AnnualLeaveDays { get; set; }
        public string MyIdentityUserId { get; set; }

    }
}
