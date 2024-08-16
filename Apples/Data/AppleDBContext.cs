using Microsoft.EntityFrameworkCore;
using Apple.Models;

namespace Apple.Data
{
    ////////////////////////////////////////////////////// DOCUMENT THIS
    public class AppleDBContext : DbContext
    {
        public AppleDBContext(DbContextOptions<AppleDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AppleDatabase.sqlite");
        }
    }
}
