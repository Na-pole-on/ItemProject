using Client.Models;
using Client.Models.Cart;

namespace Client.Services.Interfaces
{
    public interface ICartService
    {
        Task<ResponseViewModel> GetCartByUserId(string userId, string? token = null);
        Task<ResponseViewModel> AddItem(int itemId, string userId, string? token = null);
        Task<ResponseViewModel> RemoveFromCart(int cartDetailsId, string? token = null);
        Task<ResponseViewModel> CountUpper(int cartDetailsId, string? token = null);
        Task<ResponseViewModel> CountLower(int cartDetailsId, string? token = null);
    }
}
