using System.ComponentModel.DataAnnotations;

namespace ShopCartAPI.Models.Cart
{
    public class CartHeaderModel
    {
        [Key]
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
    }
}
