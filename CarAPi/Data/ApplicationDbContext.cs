using CarAPi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarAPi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Make = "Nissan",
                    Model = "Rouge",
                    Year = 2012,
                    Mileage = 10000
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "user",
                    Password = "password",
                    Role = "User"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
