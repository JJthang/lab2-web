using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models; // namespace chứa class User

namespace MyMvcApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
