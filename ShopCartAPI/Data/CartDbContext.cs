using Microsoft.EntityFrameworkCore;
using ShopCartAPI.Models;
using ShopCartAPI.Models.Cart;

namespace ShopCartAPI.Data
{
    public class CartDbContext: DbContext
    {
        public DbSet<CartHeaderModel> CartHeaders { get; set; } = null!;
        public DbSet<CartDetailsModel> CartDetails { get; set; } = null!;
        public DbSet<ItemModel> Items { get; set; } = null!;

        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options) { }
    }
}
