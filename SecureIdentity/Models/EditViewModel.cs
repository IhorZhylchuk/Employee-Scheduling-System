using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models
{
    public class EditViewModel
    {
        public string Id { get; set; }
    
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }  
        public string FullName { get; set; }   
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }

    }
}
