using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.EF;
using AutoServis.Helper;
using AutoServis.Hubs;
using AutoServis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AutoServis.Controllers
{

    public class NarudzbaController : Controller
    {
        private readonly MojContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private IHubContext<ReportsPublisher> _hubContext;
        public NarudzbaController(MojContext context,IOrderRepository orderRepository,ShoppingCart shoppingCart,IHubContext<ReportsPublisher> hubContext)
        {
            _context = context;
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _hubContext = hubContext;
        }

        public IActionResult Checkout()
        {
            IEnumerable<ShoppingCartItem> items = _shoppingCart.GetShoppingCartItems();
            var total = _shoppingCart.GetShoppingCartTotal();
            ViewBag.Items = items;
            ViewBag.Total = total;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Vasa korpa je prazna, dodajte dijelove prvo");
            }
            if (ModelState.IsValid)
            {
                var ukupno = _shoppingCart.GetShoppingCartTotal();
                if (ukupno <= 100)
                {
                    ukupno += 10;
                }
                if (ukupno > 200)
                {
                    ukupno = ukupno * 0.9;
                }
                order.Ukupno = ukupno;
                order.Zavrsena = false;
                Klijent klijent = null;
                var pretraga = _context.osoba.SingleOrDefault(x => x.Ime == order.Ime && x.Prezime == order.Prezime);
                if (pretraga != null) { klijent= _context.klijent.SingleOrDefault(x => x.OsobaId == pretraga.Id); }
                
                var grad = _context.grad.Where(g => g.Naziv == order.Grad).SingleOrDefault();
                if (grad == null)
                {
                    Grad novi = new Grad
                    {
                        Naziv = order.Grad
                    };
                    _context.grad.Add(novi);
                    _context.SaveChanges();
                }
                if (pretraga == null)
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var stringChars = new char[8];
                    var random = new Random();

                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var finalString = new String(stringChars);
                    Osoba nova = new Osoba
                    {
                        Ime = order.Ime,
                        Prezime = order.Prezime,
                        Adresa = order.Adresa,
                        KorisnickoIme = finalString,
                        Lozinka = "",
                        Telefon = order.BrojTelefona,
                        Grad = _context.grad.Where(g => g.Naziv == order.Grad).SingleOrDefault()
                    };
                    _context.osoba.Add(nova);
                    var Klijent = new Klijent
                    {
                        DatumRegistracije = order.DatumNarudzbe,
                        BrojNarudzbi = 1,
                        OsobaId = nova.Id
                    };
                    _context.klijent.Add(Klijent);
                    _orderRepository.CreateOrder(order);
                    _shoppingCart.ClearCart();
                    _context.SaveChanges();
                    //_hubContext.Clients.All.InvokeAsync("displayNotification","");
                    _hubContext.Clients.All.SendAsync("displayNotification", "");
                }
                else
                {
                    _orderRepository.CreateOrder(order);
                    _shoppingCart.ClearCart();
                    if (klijent != null) {
                        klijent.BrojNarudzbi++;
                        _context.klijent.Update(klijent);
                    }
                    _context.SaveChanges();
                    _hubContext.Clients.All.SendAsync("displayNotification", "");
                }
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }
        public IActionResult CheckoutComplete()
        {
            //_hubContext.Clients.All.SendAsync("displayNotification", "");
            ViewBag.CheckoutCompleteMessage = "Hvala Vam na Vasoj narudzbi !";
            return View();
        }
    }
}