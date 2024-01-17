using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace ShopCartAPI.Models.Cart
{
    public class CartDetailsModel
    {
        [Key]
        public int CartDetailsId { get; set; }

        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public virtual CartHeaderModel CartHeader { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual ItemModel Item { get; set; }

        public int Count { get; set; }
    }
}
