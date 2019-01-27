using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie_Tracker.Models;

namespace Movie_Tracker.Data
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

        public DbSet<Movie_Tracker.Models.Actor> Actor { get; set; }

        public DbSet<Movie_Tracker.Models.Compozitor> Compozitor { get; set; }

        public DbSet<Movie_Tracker.Models.Scenarist> Scenarist { get; set; }

        public DbSet<Movie_Tracker.Models.Regizor> Regizor { get; set; }

        public DbSet<Movie_Tracker.Models.Producator> Producator { get; set; }

        public DbSet<Movie_Tracker.Models.Film> Film { get; set; }
    }
}
