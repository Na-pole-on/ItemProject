using Microsoft.AspNetCore.Identity;

namespace ShopCartAPI.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime PaymentDate { get; set; }

        public List<ItemModel> Items { get; set; }

        public List<ItemIdsModel> ItemIdsModels { get; set; }
        public int ItemId { get; set; }

        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
