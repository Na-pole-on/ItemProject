using Client.Actions;
using Client.AutoMapper;
using Client.Models;
using Client.Services.Interfaces;
using Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IItemService _items;
        private readonly IWebHostEnvironment _webHost;

        private ListOfModels _models = new();

        public ItemsController(IItemService items,
            IWebHostEnvironment webHost) 
            => (_items, _webHost) = (items, webHost);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ItemViewModel> list = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _items.GetAllAsync(accessToken);

            if(response is not null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ItemViewModel>>(Convert.ToString(response.Result));

                return Ok(list);
            }

            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> YourItems()
        {
            List<ItemViewModel> items = new();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var sub = User.Claims.FirstOrDefault(c => c.Type.ToString() == "sub").Value;

            var response = await _items.GetByUserIdAsync(sub, accessToken);

            if (response is not null && response.IsSuccess)
            {
                items = JsonConvert.DeserializeObject<List<ItemViewModel>>(Convert.ToString(response.Result));
                
                return View(items);
            }

            return BadRequest();
        }

        public IActionResult Add()
            => View();

        [HttpPost]
        public async Task<IActionResult> Add(CreateItemViewModel add)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var item = new ItemViewModel
                {
                    Name = add.Name,
                    LongDescription = add.LongDescription,
                    CategoryName = add.CategoryName,
                    Price = add.Price,
                    Quentity = add.Quentity,
                    ShortDescription = add.ShortDescription,
                    UserId = User.Claims.FirstOrDefault(c => c.Type.ToString() == "sub").Value
                };
                item.PhotoUrl = await PhotoFileAction.AddPhoto(add.Photo, _webHost);

                var response = await _items.AddAsync(item, accessToken);

                if (response is not null && response.IsSuccess)
                    return Redirect("/Home/Index");

                return BadRequest(response);
            }

            return View(add);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UpdateItemViewModel item = new();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _items.GetByIdAsync(id, accessToken);

            if (response is not null && response.IsSuccess)
            {
                item = JsonConvert.DeserializeObject<UpdateItemViewModel>(Convert.ToString(response.Result));

                return View(item);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateItemViewModel update)
        {
            if(ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                if (update.Photo is not null)
                    update.PhotoUrl = await PhotoFileAction
                        .UpdatePhoto(update.Photo, update.PhotoUrl, _webHost);

                var item = new ItemViewModel
                {
                    Id = update.Id,
                    UserId = User.Claims.FirstOrDefault(c => c.Type.ToString() == "sub").Value,
                    Quentity = update.Quentity,
                    ShortDescription = update.ShortDescription,
                    LongDescription = update.LongDescription,
                    Name = update.Name,
                    CategoryName = update.CategoryName,
                    PhotoUrl = update.PhotoUrl,
                    Price = update.Price
                };

                var response = await _items.UpdateAsync(update.Id, item, accessToken);

                if (response is not null && response.IsSuccess)
                    return RedirectToAction("YourItems");

                return BadRequest();
            }

            return View(update);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string redirectUrl, int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _items.DeleteAsync(id, accessToken);

            if (response is not null && response.IsSuccess)
                return RedirectToAction("YourItems");

            return BadRequest(response);
        }
    }
}
