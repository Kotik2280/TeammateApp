using Microsoft.EntityFrameworkCore;
namespace TeammateApp.Models
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
