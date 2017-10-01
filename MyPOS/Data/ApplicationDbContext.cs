using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPOS.Models;

namespace MyPOS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<MyPOS.Models.MenuCategory> MenuCategory { get; set; }

        public DbSet<MyPOS.Models.MenuItem> MenuItem { get; set; }

        public DbSet<MyPOS.Models.Order> Order { get; set; }

        public DbSet<MyPOS.Models.PaymentType> PaymentType { get; set; }

        public DbSet<MyPOS.Models.Customer> Customer { get; set; }

        public DbSet<MyPOS.Models.OrderMenu> OrderMenu { get; set; }
    }
}
