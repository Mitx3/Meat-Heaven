using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class TrgovinaContext : IdentityDbContext<ApplicationUser>
    {
        public TrgovinaContext(DbContextOptions<TrgovinaContext> options) : base(options)
        {
        }

        public DbSet<Izdelek> Izdelki { get; set; }
        public DbSet<Kmetija> Kmetije { get; set; }

        public DbSet<Oddelek> Oddelki { get; set; }
        public DbSet<Kmet> Kmetje { get; set; }
        public DbSet<Zgradba> Zgradbe { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Izdelek>().ToTable("Izdelek");
            modelBuilder.Entity<Kmetija>().ToTable("Kmetija");

            modelBuilder.Entity<Zgradba>().ToTable("Zgradba");
        }

    }
}