using ShopCartAPI.Models.Cart;

namespace ShopCartAPI.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<CartModel> GetCartByUserId(string userId);
        Task<bool> AddInCart(int itemId, string userId);
        Task<bool> UpdateInCart(int itemId, string userId);
        Task<bool> CountUpper(int cartDetailsId);
        Task<bool> CountLower(int cartDetailsId);
        Task<CartDetailsModel> RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);
    }
}
