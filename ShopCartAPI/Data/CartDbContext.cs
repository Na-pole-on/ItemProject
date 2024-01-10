using Microsoft.EntityFrameworkCore;
using ShopCartAPI.Models;

namespace ShopCartAPI.Data
{
    public class CartDbContext: DbContext
    {
        public DbSet<ItemModel> Items { get; set; } = null!;
        public DbSet<CartModel> Carts { get; set; } = null!;
        public DbSet<ItemIdsModel> ItemIds { get; set; } = null!;

        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartModel>()
                .Ignore(c => c.Items)
                .Ignore(c => c.User);
        }
    }
}
