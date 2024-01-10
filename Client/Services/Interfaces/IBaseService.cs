using Client.Models;

namespace Client.Services.Interfaces
{
    public interface IBaseService: IDisposable
    {
        Task<ResponseViewModel> SendAsync(RequestViewModel request);
    }
}
