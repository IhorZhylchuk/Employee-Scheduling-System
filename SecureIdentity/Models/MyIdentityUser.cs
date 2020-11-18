﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureIdentity.Models.AnnualLeave;

namespace SecureIdentity.Models
{
    public class MyIdentityUser : IdentityUser
    {
 
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string NiN { get; set; }
        public int AnnualTotalDays { get; set; }
        public int AnnualLeaveDays { get; set; }
    }
}
