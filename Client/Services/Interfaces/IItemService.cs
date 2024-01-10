using Client.Models;

namespace Client.Services.Interfaces
{
    public interface IItemService
    {
        Task<ResponseViewModel> GetAllAsync(string? token = null);
        Task<ResponseViewModel> GetByIdAsync(int id, string? token = null);
        Task<ResponseViewModel> GetByUserIdAsync(string id, string? token = null);
        Task<ResponseViewModel> AddAsync(ItemViewModel add, string? token = null);
        Task<ResponseViewModel> UpdateAsync(int id, ItemViewModel update, string? token = null);
        Task<ResponseViewModel> DeleteAsync(int id, string? token = null);
    }
}
