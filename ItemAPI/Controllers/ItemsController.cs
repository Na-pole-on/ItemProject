using ItemAPI.Data;
using ItemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ItemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly ItemDbContext _context;

        public ItemsController(ItemDbContext context)
            => _context = context;

        [HttpGet]
        public ResponseModel GetAll()
            => new ResponseModel
            {
                Result = _context.Items,
            };

        [HttpGet("{id:int}")]
        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                var item = await _context.Items.FindAsync(id);

                return new ResponseModel
                {
                    Result = item
                };
            }
            catch(Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
            }
        }

        [HttpGet("{sub:Guid}")]
        public ResponseModel GetByUserId(Guid sub)
        {
            try
            {
                var item = _context.Items
                    .Where(i => i.UserId == sub.ToString());

                return new ResponseModel
                {
                    Result = item
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> Add([FromBody]ItemModel add)
        {
            try
            {
                await _context.Items.AddAsync(add);
                await _context.SaveChangesAsync();

                return new ResponseModel { Result = add };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<ResponseModel> Update(int id, [FromBody]ItemModel update)
        {
            try
            {
                update.Id = id;

                _context.Items.Update(update);
                await _context.SaveChangesAsync();

                return new ResponseModel { Result = update };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            try
            {
                var item = await _context.Items.FindAsync(id);
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();

                return new ResponseModel { Result = item };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
            }
        }
        
    }
}
