using Microsoft.EntityFrameworkCore;
using Models;

namespace Product.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<ProductItem>? ProductItems { get; set; }
        public DbSet<CarritoItem>? CarritoItems { get; set; }
        public DbSet<Order>? Orders{ get; set; }
        public DbSet<OrderItem>? OrderItems{ get; set; }
    }
}
