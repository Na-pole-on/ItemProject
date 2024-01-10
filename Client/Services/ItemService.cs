using Client.Models;
using Client.Services.Interfaces;

namespace Client.Services
{
    public class ItemService: BaseService, IItemService
    {
        private IConfiguration _configuration;

        public ItemService(IHttpClientFactory http, IConfiguration configuration): base(http) 
            => _configuration = configuration;

        public async Task<ResponseViewModel> GetAllAsync(string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Get,
                Url = _configuration["apiUrl"] + "/api/items",
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> GetByIdAsync(int id, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Get,
                Url = _configuration["apiUrl"] + "/api/items/" + id,
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> GetByUserIdAsync(string id, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Get,
                Url = _configuration["apiUrl"] + "/api/items/" + id,
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> AddAsync(ItemViewModel add, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Post,
                Url = _configuration["apiUrl"] + "/api/items",
                Data = add,
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> UpdateAsync(int id, ItemViewModel update, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Put,
                Url = _configuration["apiUrl"] + "/api/items/" + id,
                Data = update,
                AccessToken = token ?? string.Empty
            });

        public async Task<ResponseViewModel> DeleteAsync(int id, string? token = null)
            => await this.SendAsync(new RequestViewModel
            {
                Method = HttpMethod.Delete,
                Url = _configuration["apiUrl"] + "/api/items/" + id,
                AccessToken = token ?? string.Empty
            });
    }
}
