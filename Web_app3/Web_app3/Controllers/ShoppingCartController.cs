using System.Linq;
using AutoServis.EF;
using AutoServis.Models;
using AutoServis.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoServis.Controllers
{
    public class ShoppingCartController : Controller
    {
        

        private readonly MojContext _context;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(MojContext context,ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var scvm = new ShoppingCartVM
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
        return View(scvm); 
        }
        public RedirectToActionResult AddToShoppingCart(int DioID)
        {
            var selectedDio = _context.dio.FirstOrDefault(p => p.DioId == DioID);
            if (selectedDio != null)
            {
                _shoppingCart.AddToCart(selectedDio, 1);
            }
            return RedirectToAction("Index","ShoppingCart");
        }
        public RedirectToActionResult Increment(int DioID)
        {
            var selectedDio = _context.dio.FirstOrDefault(p => p.DioId == DioID);
            if (selectedDio != null)
            {
                _shoppingCart.AddToCart(selectedDio, 1);
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int DioID)
        {
            var selectedDio = _context.dio.FirstOrDefault(p => p.DioId == DioID);
            if (selectedDio != null)
            {
                _shoppingCart.RemoveFromCart(selectedDio);
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveAmount(int DioID)
        {
            var selectedDio = _context.dio.FirstOrDefault(p => p.DioId == DioID);
            if (selectedDio != null)
            {
                _shoppingCart.RemoveAmount(selectedDio);
            }
            return RedirectToAction("Index");
        }
    }
}