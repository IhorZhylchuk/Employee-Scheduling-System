using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models
{
    public class IndexViewModel
    {
        public IEnumerable<MyIdentityUser> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
    }
}
