using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Models.AnnualLeave;
using SecureIdentity.Models.WorkingShift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models
{
    public class MyIdentityDbContext : IdentityDbContext<MyIdentityUser, MyIdentityRole, string>
    {
        public DbSet<AnnualLeaveCount> AnnualLeaves { get; set; }
        public DbSet<AnnualLeaveViewModel> AnnualLeaveViews { get; set; }
        public DbSet<WorkingDay> WorkingDays { get; set; }
        public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
