using Microsoft.EntityFrameworkCore;
using Product;
using CarritoItem;
using OrderItem;
namespace Product.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<ProductItems>? ProductI { get; set; }
        public DbSet<CarritoItems>? CarritoItem { get; set; }
        public DbSet<OrderItems>? OrderItem { get; set; }
    }
}
