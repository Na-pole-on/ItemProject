using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopCartAPI.Data;
using ShopCartAPI.Models;
using ShopCartAPI.Models.Cart;
using ShopCartAPI.Repositories.Interfaces;

namespace ShopCartAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        private ResponseModel _response;

        public CartController(ICartRepository cartRepository)
            => (_cartRepository, _response) = (cartRepository, new());

        [HttpGet("{userId}")]
        public async Task<ResponseModel> GetCart(string userId)
        {
            try
            {
                _response.Result = await _cartRepository
                    .GetCartByUserId(userId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
            }

            return _response;
        }

        [HttpPost]
        public async Task<ResponseModel> AddInCart([FromBody] RequestIdsModel ids)
        {
            try
            {
                _response.Result = await _cartRepository
                    .AddInCart(ids.ItemId, ids.UserId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
            }

            return _response;
        }

        [HttpPost]
        public async Task<ResponseModel> UpdateInCart([FromBody] RequestIdsModel ids)
        {
            try
            {
                _response.IsSuccess = await _cartRepository
                    .UpdateInCart(ids.ItemId, ids.UserId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
            }

            return _response;
        }

        [HttpPut("{cartDetailId}")]
        public async Task<ResponseModel> CountUpper(int cartDetailId)
        {
            try
            {
                _response.IsSuccess = await _cartRepository
                    .CountUpper(cartDetailId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
            }

            return _response;
        }

        [HttpPut("{cartDetailId}")]
        public async Task<ResponseModel> CountLower(int cartDetailId)
        {
            try
            {
                _response.IsSuccess = await _cartRepository
                    .CountLower(cartDetailId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
            }

            return _response;
        }

        [HttpDelete("{cartDetailsId}")]
        public async Task<ResponseModel> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                _response.Result = await _cartRepository.RemoveFromCart(cartDetailsId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
            }

            return _response;
        }

        [HttpDelete("{userId}")]
        public async Task<ResponseModel> RemoveCart(string userId)
        {
            try
            {
                _response.Result = await _cartRepository.ClearCart(userId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
            }

            return _response;
        }
    }
}
