using Microsoft.EntityFrameworkCore;

namespace First.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<Information> info { get; set; }
    }
}
