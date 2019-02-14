using AutoServis.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AutoServis.Models
{
    public class ShoppingCart
    {
        private readonly MojContext _context;
        public ShoppingCart(MojContext context)
        {
            _context = context;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<MojContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Dio dio,int amount)
        {
            var ShoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Dio.DioId == dio.DioId && s.ShoppingCartId == ShoppingCartId);
            if (ShoppingCartItem == null)
            {
                ShoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Dio = dio,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Amount += amount;
            }
            _context.SaveChanges();
        }
        public void RemoveAmount(Dio dio)
        {
            var ShoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Dio.DioId == dio.DioId && s.ShoppingCartId == ShoppingCartId);
            if (ShoppingCartItem != null)
            {
               _context.ShoppingCartItems.Remove(ShoppingCartItem);
            }
            _context.SaveChanges();
        }
        public int RemoveFromCart(Dio dio)
        {
            var ShoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Dio.DioId == dio.DioId && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;

            if (ShoppingCartItem != null)
            {
                if (ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount--;
                    localAmount = ShoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(ShoppingCartItem);
                }
            }
            _context.SaveChanges();
            return localAmount;
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            
            return ShoppingCartItems ??
                (ShoppingCartItems = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId)
                .Include(g => g.Dio)
                .Include(g=>g.Dio.model)
                .Include(g=>g.Dio.model.marka)
                .ToList());
        }
        public void ClearCart()
        {
            var cartItems = _context
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _context.ShoppingCartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(f => f.Dio.Cijena * f.Amount).Sum();
            return total;
        }
    }
}
