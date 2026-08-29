using Microsoft.EntityFrameworkCore;

namespace ASP_net_Project.Models
{
    public class LfmsContext : DbContext
    {

        public LfmsContext(DbContextOptions<LfmsContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

    }
}
