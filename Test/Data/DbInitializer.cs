using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data.Entities;

namespace Test.Data
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (!_context.Menus.Any())
            {
                var menus = new List<Menu>
                {
                    new Menu
                    {
                        Name = "Menu 1"
                    },
                    new Menu
                    {
                        Name = "Menu 2"
                    },
                };

                _context.Menus.AddRange(menus);
            }
            await _context.SaveChangesAsync();
        }
    }
}