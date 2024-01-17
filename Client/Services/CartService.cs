using Client.Models;
using Client.Models.Cart;
using Client.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace Client.Services
{
    public class CartService: BaseService, ICartService
    {
        private IConfiguration _configuration;
        
        public CartService(IHttpClientFactory http, IConfiguration configuration): 
            base(http) => _configuration = configuration;

        public async Task<ResponseViewModel> GetCartByUserId(string userId, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Get,
                Url = _configuration["CartAPI"] + "/api/cart/GetCart/" + userId,
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> AddItem(int itemId, string userId, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Post,
                Url = _configuration["CartAPI"] + "/api/cart/AddInCart",
                Data = new RequestIdsViewModel { ItemId = itemId, UserId = userId },
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> RemoveFromCart(int cartDetailsId, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Delete,
                Url = _configuration["CartAPI"] + "/api/cart/RemoveFromCart/" + cartDetailsId,
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> CountUpper(int cartDetailsId, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Put,
                Url = _configuration["CartAPI"] + "/api/cart/CountUpper/" + cartDetailsId,
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> CountLower(int cartDetailsId, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Put,
                Url = _configuration["CartAPI"] + "/api/cart/CountLower/" + cartDetailsId,
                AccessToken = token ?? string.Empty
            });
    }
}
