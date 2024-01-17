using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Client.Models.Items;

namespace Client.Models.Cart
{
    public class CartDetailsViewModel
    {
        [Key]
        public int CartDetailsId { get; set; }

        public int ItemId { get; set; }
        public ItemViewModel Item { get; set; }

        public int Count { get; set; }
    }
}
