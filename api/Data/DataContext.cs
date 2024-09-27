using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        
        }

        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Определяем связь один-ко-многим между AppUser и Card
            builder.Entity<Card>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Cards)
                .HasForeignKey(c => c.AppUserId);
        }
    }
}
