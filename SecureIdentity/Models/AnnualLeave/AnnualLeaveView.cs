using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models.AnnualLeave
{
    public class AnnualLeaveView : AnnualLeaveViewModel
    {
        [Required]
        public List<string> Users { get; set; }
    }
}
