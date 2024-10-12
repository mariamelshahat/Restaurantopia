using Microsoft.EntityFrameworkCore;
using Restaurantopia.Entities.Models;

namespace Restaurantopia.Models
{
    public class MyDbContext : DbContext {
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {


    }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Review> Review { get; set; }
       
    }
}
