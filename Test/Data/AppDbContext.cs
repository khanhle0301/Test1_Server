using Microsoft.EntityFrameworkCore;
using Test.Data.Entities;

namespace Test.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Menu> Menus { set; get; }
    }
}