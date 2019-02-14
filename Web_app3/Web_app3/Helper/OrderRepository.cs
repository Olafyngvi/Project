using AutoServis.EF;
using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Helper
{
    public class OrderRepository :IOrderRepository
    {
        private readonly MojContext _context;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(MojContext context,ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.DatumNarudzbe = DateTime.Now;
            order.Zavrsena = false;
            _context.Orders.Add(order);
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach(var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetails()
                {
                    Kolicina = item.Amount,
                    DioId = item.Dio.DioId,
                    OrderId = order.OrderId,
                    Cijena = item.Dio.Cijena
                };
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();
        }
    }
}
