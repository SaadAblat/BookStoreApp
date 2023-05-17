using BookStore.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories.Customer
{
    public class UserRepository : IDisposable
    {
        private readonly DataContext _ctx;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AuthenticationStateProvider _authenticationStateAsync;
        public UserRepository(DataContext ctx, UserManager<ApplicationUser> userManager,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _ctx = ctx;
            _userManager = userManager;
            _authenticationStateAsync = authenticationStateProvider;
        }
        public void Dispose()
        {
            _ctx.Dispose();
        }

        public async Task<ApplicationUser> GetUserAsync()
        {
            var authState = await _authenticationStateAsync.GetAuthenticationStateAsync();
            var user = authState.User;
            var applicationUser = new ApplicationUser();
            if (user != null)
            {
                applicationUser = await _userManager.Users
                    .Include(u=>u.Reviews)
                    .Include(u=>u.Orders)
                    .Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems)
                    .ThenInclude(ci => ci.Book)
                    .FirstOrDefaultAsync(u => u.Id == user.Claims.FirstOrDefault().Value);

                // If the ApplicationUser does not have a Cart, create a new one
                if (applicationUser != null && applicationUser.Cart == null)
                {
                    applicationUser.Cart = new Cart { UserId = applicationUser.Id };
                    _ctx.Carts.Add(applicationUser.Cart);
                    await _ctx.SaveChangesAsync();
                }
            }
            return applicationUser;
            
        }



        public async Task<CartItem> AddToCartAsync(Book book, ApplicationUser user)
        {
            var cart = user.Cart;
            if (cart == null)
            {
                cart = new Cart { UserId = user.Id };
                _ctx.Carts.Add(user.Cart);
                await _ctx.SaveChangesAsync();
            }

            if (await _ctx.CartItems.AnyAsync(ci => ci.BookId == book.ID))
            {
                var existantCartItem = await _ctx.CartItems.FirstAsync(ci => ci.BookId == book.ID);
                existantCartItem.Quantity += 1;
                cart.CartItems.FirstOrDefault(ci => ci.BookId == book.ID).Quantity = existantCartItem.Quantity;
                await _ctx.SaveChangesAsync();
                return existantCartItem;
            }
            else
            {
                var cartItem = new CartItem()
                {
                    CartId = cart.ID,
                    BookId = book.ID,
                    Quantity = 1
                };
                _ctx.CartItems.Add(cartItem);
                await _ctx.SaveChangesAsync();
                return cartItem;
            }
        }

        public async Task RemoveItemFromCart(CartItem item)
        {
            _ctx.CartItems.Remove(item);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateItem(CartItem updatedItem)
        {
            var oldItem = _ctx.CartItems.FirstOrDefault(ci => ci.ID == updatedItem.ID);
            oldItem.Quantity = updatedItem.Quantity;
            await _ctx.SaveChangesAsync();
        }
    }
}
