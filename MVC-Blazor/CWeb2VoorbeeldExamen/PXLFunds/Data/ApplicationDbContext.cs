using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PXLFunds.Data;

namespace PXLFunds.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PXLFunds.Data.Bank> Bank { get; set; }
        public DbSet<PXLFunds.Data.Fund> Fund { get; set; }
        public DbSet<PXLFunds.Data.UserFund> UserFund { get; set; }
    }
}
