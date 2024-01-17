namespace Client.Models.Cart
{
    public class CartViewModel
    {
        public CartHeaderViewModel CartHeader { get; set; }

        public List<CartDetailsViewModel> CartDetails { get; set; }
        public decimal Price { get; set; } = 0;
        public int Quentity { get; set; } = 0;
    }
}
