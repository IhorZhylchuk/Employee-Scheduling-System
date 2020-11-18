using SecureIdentity.Models.AnnualLeave;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models
{
    public class AnnualLeaveViewModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string ToDate { get; set; }
        //[Required]
        //public List<string> Users { get; set; }
        [StringLength(1000)]
        public string Comment { get; set; }
        public MyIdentityUser User { get; set; }
       // public int AnnualLeaveCountId { get; set; }
       // public AnnualLeaveCount AnnualLeaveCount { get; set; }
    }
}
