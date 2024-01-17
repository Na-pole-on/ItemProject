using Microsoft.EntityFrameworkCore;
using ShopCartAPI.Data;
using ShopCartAPI.Models;
using ShopCartAPI.Models.Cart;
using ShopCartAPI.Repositories.Interfaces;
using System.Reflection.PortableExecutable;

namespace ShopCartAPI.Repositories
{
    public class CartRepository: ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext context) 
            => _context = context;

        public async Task<CartModel> GetCartByUserId(string userId)
        {
            CartModel cart = new();

            try
            {
                cart.CartHeader = await _context.CartHeaders
                    .FirstOrDefaultAsync(ch => ch.UserId == userId);

                cart.CartDetails = _context.CartDetails
                    .Where(cd => cd.CartHeaderId == cart.CartHeader.CartHeaderId).Include(cd => cd.Item);

                if (cart.CartHeader is null)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                cart.CartHeader = new CartHeaderModel { UserId = userId };
                await _context.CartHeaders.AddAsync(cart.CartHeader);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<bool> AddInCart(int itemId, string userId)
        {
            CartHeaderModel? cartHeader = await _context.CartHeaders
                .FirstOrDefaultAsync(ch => ch.UserId == userId);

            CartDetailsModel? cartDetails = await _context.CartDetails.Include(cd => cd.Item)
                .FirstOrDefaultAsync(ch => ch.CartHeaderId == cartHeader.CartHeaderId && ch.ItemId == itemId);

            if(cartDetails is not null)
                return false;

            ItemModel? item = await _context.Items
                .FindAsync(itemId);

            if (item is null || cartHeader is null)
                return false;

            cartDetails = new CartDetailsModel
            {
                Item = item,
                CartHeader = cartHeader,
                Count = 1
            };
            await _context.CartDetails.AddAsync(cartDetails);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateInCart(int itemId, string userId)
        {
            ItemModel item = await _context.Items.FindAsync(itemId);

            CartHeaderModel? cartHeader = await _context.CartHeaders
                .FirstOrDefaultAsync(ch => ch.UserId == userId);

            CartDetailsModel? cartDetails = await _context.CartDetails
                .FirstOrDefaultAsync(cd => cd.CartHeaderId == cartHeader.CartHeaderId &&
                cd.ItemId == itemId);

            if (item.Quentity - cartDetails.Count + 1 >= 0)
                cartDetails.Count += 1;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CountUpper(int cartDetailId)
        {
            CartDetailsModel? cartDetails = await _context.CartDetails
                .FirstOrDefaultAsync(cd => cd.CartDetailsId == cartDetailId);

            cartDetails.Item = await _context.Items
                .FirstOrDefaultAsync(cd => cd.Id == cartDetails.ItemId);

            if (cartDetails.Count + 1 > cartDetails.Item.Quentity)
                return false;

            cartDetails.Count += 1;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CountLower(int cartDetailId)
        {
            CartDetailsModel? cartDetails = await _context.CartDetails
                .FirstOrDefaultAsync(cd => cd.CartDetailsId == cartDetailId);

            if (cartDetails.Count == 0)
                return false;

            cartDetails.Count -= 1;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CartDetailsModel> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetailsModel? cartDetails = await _context.CartDetails
                    .Include(cd => cd.Item)
                    .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItems = _context.CartDetails
                    .Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();

                return cartDetails;
            }
            catch (Exception ex)
            {
                return new CartDetailsModel();
            }
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartheaderFromDb = await _context.CartHeaders
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (cartheaderFromDb is not null)
            {
                _context.CartDetails
                    .RemoveRange(_context.CartDetails
                        .Where(u => u.CartHeaderId == cartheaderFromDb.CartHeaderId));

                _context.CartHeaders.Remove(cartheaderFromDb);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
