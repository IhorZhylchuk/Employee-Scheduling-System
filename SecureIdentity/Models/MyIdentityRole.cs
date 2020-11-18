using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models
{
    public class MyIdentityRole : IdentityRole
    {
       // public string Description { get; set; }
        public string Role { get; set; }
        public int RoleID { get; set; }
        //public int UserId { get; set; }
    }
}
