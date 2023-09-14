using ApiFinal.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiFinal.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
