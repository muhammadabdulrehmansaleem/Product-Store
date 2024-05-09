using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly DbContextOptions _options;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _options = options;
        }
        public DbSet<WebApplication1.Models.Products.Products> Product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WebApplication1.Models.Products.Products>(entity =>
            {
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)");
                SeedRoles(modelBuilder);
            });
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
            );
        }
    }
}
