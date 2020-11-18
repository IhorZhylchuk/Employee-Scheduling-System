using Microsoft.AspNet.Identity;
using SecureIdentity.Models.AnnualLeave;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models
{
    public class RegisterViewModel
    {
 
     //   [Required]
        public string UserName { get; set; }
        /*
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        */
        [Required]
        [StringLength(11,MinimumLength = 11)]
        public string NiN { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Role { get; set; }

        [Required]
        public int AnnualTotalDays { get; set; }
        [Required]
        public int AnnualLeaveDays { get; set; }

        //public MyIdentityRole Role { get; set; }  
    }
}
