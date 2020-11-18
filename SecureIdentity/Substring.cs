using SecureIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity
{
    public class Substring
    {
        /*
        public string Sub(MyIdentityDbContext context, string id)
        {
            string[] fullname = context.Users.Where(i => i.Id == id).Select(f => f.FullName).ToArray();
            var date = context.Users.Where(i => i.Id == id).Select(f => f.DateOfBirth).ToArray();
            var space = fullname[0].IndexOf(" ");
            string name = fullname[0].Substring(0,space);
            string surname = fullname[0].Substring(space+1, 3);
            return name + surname + date[0].ToString("MM'/'dd'/'yyyy HH':'mm':'ss.fff").Substring(0,2);
        }
        */
        public string Sub(DateTime date, string fullname)
        {
            //string[] fullname = context.Users.Where(i => i.Id == id).Select(f => f.FullName).ToArray();
            // date = context.Users.Where(i => i.Id == id).Select(f => f.DateOfBirth).ToArray();
            var space = fullname.IndexOf(" ");
            string name = fullname.Substring(0, space);
            string surname = fullname.Substring(space + 1, 3);
            return name + surname + date.ToString("MM'/'dd'/'yyyy HH':'mm':'ss.fff").Substring(0, 2);
        }
    }
}
