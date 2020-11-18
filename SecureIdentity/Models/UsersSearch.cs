using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models
{
    public class UsersSearch
    {
        public List<MyIdentityUser> Users { get; set; }


        public string UserName { get; set; }
    }
}
