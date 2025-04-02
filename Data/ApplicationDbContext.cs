using DiplomMetod.Data.Configurations;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<ReferenceBook> ReferenceBooks { get; set; }
        public DbSet<FormType> Forms { get; set; }
        public DbSet<RegionDivision> RegionsDivisions { get; set; }

        public DbSet<Organization> Organizations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FormConfiguration());
        }

    }
}
