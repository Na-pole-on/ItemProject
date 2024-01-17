using Client.Actions;
using Client.Models.Cart;
using Client.Models.Items;
using Client.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace Client.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
            => _cartService = cartService;

        [HttpGet]
        public IActionResult ShoppingCart()
            => View();

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            CartViewModel cart = new();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var sub = User.FindFirstValue("sub");
            var json = await _cartService.GetCartByUserId(sub, accessToken);

            if (json is not null)
                cart = JsonConvert.DeserializeObject<CartViewModel>(Convert.ToString(json.Result));

            if(cart is not null)
            {
                cart.Price = DetailsAction.GetPrice(cart.CartDetails);
                cart.Quentity = DetailsAction.GetQuentity(cart.CartDetails);
            }

            return Ok(cart);
        }

        [HttpGet("/Cart/AddInCart/{itemId}")]
        public async Task<IActionResult> AddInCart(int itemId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var sub = User.FindFirstValue("sub");

            var json = await _cartService.AddItem(itemId, sub, accessToken);

            return Json(itemId);
        }

        [HttpGet("/Cart/RemoveFromCart/{cartDetailsCart}")]
        public async Task<IActionResult> RemoveFromCart(int cartDetailsCart)
        {
            CartDetailsViewModel cartDetails = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var json = await _cartService.RemoveFromCart(cartDetailsCart, accessToken);

            if (json is not null)
                cartDetails = JsonConvert.DeserializeObject<CartDetailsViewModel>(Convert.ToString(json.Result));

            return Ok(cartDetails);
        }

        [HttpGet("/Cart/CountUpper/{cartDetailsCart}")]
        public async Task<IActionResult> CountUpper(int cartDetailsCart)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var json = await _cartService.CountUpper(cartDetailsCart, accessToken);

            if (json.IsSuccess)
                return Ok();

            return BadRequest();
        }

        [HttpGet("/Cart/CountLower/{cartDetailsCart}")]
        public async Task<IActionResult> CountLower(int cartDetailsCart)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var json = await _cartService.CountLower(cartDetailsCart, accessToken);

            if (json.IsSuccess)
                return Ok();

            return BadRequest();
        }
    }
}
