using ItemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemAPI.Data
{
    public class ItemDbContext: DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options) { }

        public DbSet<ItemModel> Items { get; set; } = null!;
    }
}
