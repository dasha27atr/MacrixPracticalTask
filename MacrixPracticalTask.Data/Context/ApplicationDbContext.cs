using MacrixPracticalTask.Models;
using MacrixPracticalTask.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace MacrixPracticalTask.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People => Set<Person>();
        public DbSet<Log> Logs => Set<Log>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
                entity.Property(e => e.FirstName);
                entity.Property(e => e.LastName);
                entity.Property(e => e.StreetName);
                entity.Property(e => e.HouseNumber).HasMaxLength(500);
                entity.Property(e => e.ApartmentNumber).HasMaxLength(1000);
                entity.Property(e => e.PostalCode).HasMaxLength(1000000);
                entity.Property(e => e.Town);
                entity.Property(e => e.PhoneNumber);
                entity.Property(e => e.DateOfBirth);
            });
        }
    }
}
