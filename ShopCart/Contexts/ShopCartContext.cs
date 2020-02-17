using Microsoft.EntityFrameworkCore;

namespace ShopCart.Contexts
{
    public class ShopCartContext : DbContext
    {
        public ShopCartContext(DbContextOptions<ShopCartContext> options) : base(options) {}

        public DbSet<Models.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Models.Customer>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}