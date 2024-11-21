using gamesssss.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gamesssss.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for entities
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        public DbSet<Review> Reviews { get; set; } // Add this line for the one-to-many relationship

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding Category and Device data
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
        new Category {Id = 1, Name = "Sports"},
        new Category {Id = 2, Name = "Action"},
        new Category {Id = 3, Name = "Adventure"},
        new Category {Id = 4, Name = "Racing"},
        new Category {Id = 5, Name = "Film"},
        new Category {Id = 6, Name = "Fighting"}
            });

            modelBuilder.Entity<Device>().HasData(new Device[]
            {
        new Device {Id = 1, Name = "PlayStation", Icon = "bi bi-playstation"},
        new Device {Id = 2, Name = "Xbox", Icon = "bi bi-xbox"},
        new Device {Id = 3, Name = "Nintendo Switch", Icon = "bi bi-nintendo-switch"},
        new Device {Id = 4, Name = "PC", Icon = "bi bi-pc-display"}
            });

            // Define many-to-many relationship between Game and Device
            modelBuilder.Entity<GameDevice>().HasKey(e => new { e.GameId, e.DviceId });


            modelBuilder.Entity<Review>()
         .HasOne(r => r.Game) // Review has one Game
         .WithMany(g => g.Reviews) // Game has many Reviews
         .HasForeignKey(r => r.GameId); // Foreign key for GameId

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User) // Review has one User (from Identity)
                .WithMany(u => u.Reviews) // User can have many Reviews (specified Reviews navigation property)
                .HasForeignKey(r => r.UserId) // Foreign key for UserId
                .OnDelete(DeleteBehavior.Cascade); // Optional: Define the delete behavior

            base.OnModelCreating(modelBuilder);
        }
    }
}
