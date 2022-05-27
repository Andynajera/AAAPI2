using Microsoft.EntityFrameworkCore;
using Product;
using CarritItem;
using OrderItem;
namespace Product.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<ProductItems>? ProducItem { get; set; }
        public DbSet<CarritoItems>? CarritItem { get; set; }
        public DbSet<OrderItems>? OrdeItem { get; set; }
    }
}
