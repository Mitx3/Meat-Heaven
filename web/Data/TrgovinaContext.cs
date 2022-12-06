using web.Models;
using Microsoft.EntityFrameworkCore;

namespace web.Data
{
    public class TrgovinaContext : DbContext
    {
        public TrgovinaContext(DbContextOptions<TrgovinaContext> options) : base(options)
        {
        }

        public DbSet<Izdelek> Izdelki { get; set; }
        public DbSet<Kmetija> Kmetije { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Izdelek>().ToTable("Izdelek");
            modelBuilder.Entity<Kmetija>().ToTable("Kmetija");
        }

    }
}