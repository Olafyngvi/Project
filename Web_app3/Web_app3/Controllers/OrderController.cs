using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoServis.Helper;
using AutoServis.Models;
using AutoServis.EF;
using Microsoft.EntityFrameworkCore;
using AutoServis.ViewModels;
using Microsoft.AspNetCore.SignalR;
using AutoServis.Hubs;
using Nexmo.Api;

namespace AutoServis.Controllers
{
    [Autorizacija(false, false, true, false)]
    public class OrderController : Controller
    {
        private MojContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private IHubContext<ReportsPublisher> _hubContext;

        //Constructor
        public OrderController(MojContext context,IOrderRepository orderRepository,ShoppingCart shoppingCart, IHubContext<ReportsPublisher> hubContext)
        {
            _context = context;
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _hubContext = hubContext;
        }
        public bool Za { get; set; }
        public  IActionResult Index(string sort)
        {
            Za = false;
            var orderIme = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            var orderDatum = sort == "Date" ? "desc_date" : "Date";

                List<Order> _narudzbe = _context.Orders.Where(s=>s.Zavrsena==false).ToList();
                switch (sort)
                {
                    case "name_desc":
                        _narudzbe = _narudzbe.OrderByDescending(s => s.Ime).Where(c => c.Zavrsena == false).ToList();
                        break;
                    case "Date":
                        _narudzbe = _narudzbe.OrderBy(s => s.DatumNarudzbe).Where(c => c.Zavrsena == false).ToList();
                        break;
                    case "desc_date":
                        _narudzbe = _narudzbe.OrderByDescending(s => s.DatumNarudzbe).Where(s => s.Zavrsena == false).ToList();
                        break;
                    default:
                        _narudzbe = _narudzbe.OrderBy(s => s.Ime).ToList();
                        break;
                }

                ViewData["Zarada"] = _context.Orders.Where(x=>x.Zavrsena==true).Sum(x => x.Ukupno);
                ViewData["ZaradaTrenutni"] = _context.Orders.Where(x=>x.DatumNarudzbe.Month==DateTime.Now.Month&& x.Zavrsena==true).Sum(x => x.Ukupno);
                ViewData["Ukupno"] = _context.Orders.Count();
                ViewData["vrsta"] = "Narudžbe na čekanju";
                ViewData["Zavrsena"] = Za;
                ViewData["OrderIme"] = orderIme;
                ViewData["OrderDatum"] = orderDatum;
                //var mojContext = _context.Orders.Include(f => f.OrderLines).Where(c => c.Zavrsena == true);
                return View(_narudzbe);

        }
        public IActionResult Index2(string sort)
        {
            Za = true;
            var orderIme = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            var orderDatum = sort == "Date" ? "desc_date" : "Date";

            List<Order> _narudzbe = _context.Orders.Where(s => s.Zavrsena == true).ToList();
            switch (sort)
            {
                case "name_desc":
                    _narudzbe = _narudzbe.OrderByDescending(s => s.Ime).Where(c => c.Zavrsena == true).ToList();
                    break;
                case "Date":
                    _narudzbe = _narudzbe.OrderBy(s => s.DatumNarudzbe).Where(c => c.Zavrsena == true).ToList();
                    break;
                case "desc_date":
                    _narudzbe = _narudzbe.OrderByDescending(s => s.DatumNarudzbe).Where(s => s.Zavrsena == true).ToList();
                    break;
                default:
                    _narudzbe = _narudzbe.OrderBy(s => s.Ime).ToList();
                    break;
            }
            ViewData["Ukupno"] = _context.Orders.Count();
            ViewData["vrsta"] = "Završene narudžbe";
            ViewData["Zavrsena"] = Za;
            ViewData["OrderIme"] = orderIme;
            ViewData["OrderDatum"] = orderDatum;
            //var mojContext = _context.Orders.Include(f => f.OrderLines).Where(c => c.Zavrsena == true);
            return View(_narudzbe);

        }
        public async Task<IActionResult> NarudzbeByKlijent(int id)
        {
            var klijent = await _context.klijent.Include(x=>x.Osoba).Where(x => x.Id == id).SingleOrDefaultAsync();
            var narudzbe = await _context.Orders.Where(y => y.Ime == klijent.Osoba.Ime && y.Prezime == klijent.Osoba.Prezime).ToListAsync();
            return PartialView("_NarudzbeKlijenti",narudzbe);
        }
        public async Task<IActionResult> DijeloviDetalji(int id,double ukupno)
        {
            var detalji = await _context.OrderDetails.Include(x => x.Dio)
                                                     .Include(y=>y.Dio.model)
                                                     .Include(c=>c.Dio.model.marka)
                                                     .Where(x => x.OrderId == id).ToListAsync();
            ViewBag.Ukupno = ukupno;
            return PartialView("_DijeloviDetalji", detalji);

        }
        public IActionResult Parcijalni(bool? zavrsene, string sort,string pretraga)
        {
            var orderIme = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            var orderDatum = sort == "Date" ? "desc_date" : "Date";

            if (!string.IsNullOrEmpty(pretraga))
            {
                var pr = _context.Orders.Where(s => s.Ime.Contains(pretraga) || s.Prezime.Contains(pretraga));
                return PartialView("_Narudzbe", pr);
            }
            if (zavrsene == true)
            {
                ViewData["OrderIme"] = orderIme;
                ViewData["OrderDatum"] = orderDatum;
                ViewData["Zavrsena"] = zavrsene;
                var mojContext = _context.Orders.Include(f => f.OrderLines).Where(c => c.Zavrsena == true);
                return PartialView("_Narudzbe", mojContext);
            }
            else
            {
                ViewData["OrderIme"] = orderIme;
                ViewData["OrderDatum"] = orderDatum;
                ViewData["Zavrsena"] = zavrsene;
                var _mojContext = _context.Orders.Include(f => f.OrderLines).Where(c => c.Zavrsena == false);
                return PartialView("_Narudzbe", _mojContext);
            }

        }
        public IActionResult Zavrsi(int id)
        {
            if (id != 0) {
            var order = _context.Orders.SingleOrDefault(w => w.OrderId == id);
                var dijelovi = _context.OrderDetails.Include(x=>x.Dio).Where(x => x.OrderId == order.OrderId).ToList();
                order.Zavrsena = true;
            _context.Update(order);
            _context.SaveChanges();
                var client = new Client(creds: new Nexmo.Api.Request.Credentials
                {
                    ApiKey = "1b624e6c",
                    ApiSecret = "Eav3YsIzJYZTi1l7"
                });
                var broj = order.BrojTelefona;
                var results = client.SMS.Send(request: new SMS.SMSRequest
                {

                    from = "Auto Salon NER",
                    to = broj,
                    text = "Postovani " + order.Ime + ", Vasa narudzba na dan " + order.DatumNarudzbe.ToShortDateString()
                    .ToString()+
                    " je obradjena i biti ce na Vasoj adresi u naredna 24 sata.Hvala na povjerenju!"
                
            });
                _hubContext.Clients.All.SendAsync("displayNotification", "");
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

    }
        public IActionResult Detalji(int id)
        {
            var x = _context.Orders.SingleOrDefault(s => s.OrderId == id);
            var dijelovi = _context.OrderDetails.Include(g => g.Dio)
                                                .Include(v => v.Dio.model)
                                                .Include(w => w.Dio.model.marka)
                                                .Where(t => t.OrderId == id).ToList();
            var osoba = _context.klijent.Where(g => g.Osoba.Ime == x.Ime && g.Osoba.Prezime == x.Prezime).SingleOrDefault();
            if (osoba != null)
            {
                var model = new NarudzbaDijeloviViewModel()
                {
                    NarudzbaId = x.OrderId,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    Grad = x.Grad,
                    ZipCode = x.ZipCode,
                    Drzava = x.Drzava,
                    BrojTelefona = x.BrojTelefona,
                    BrojNarudzbi = osoba.BrojNarudzbi,
                    Email = x.Email,
                    DatumNarudzbe = x.DatumNarudzbe,
                    Dijelovi = dijelovi,
                    Ukupno = x.Ukupno,
                    Zavrsena = x.Zavrsena
                };
                return PartialView("_NarudzbaDetalji", model);
            }
            else
            {
                var model = new NarudzbaDijeloviViewModel()
                {
                    NarudzbaId = x.OrderId,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    Grad = x.Grad,
                    ZipCode = x.ZipCode,
                    Drzava = x.Drzava,
                    BrojTelefona = x.BrojTelefona,
                    BrojNarudzbi = 0,
                    Email = x.Email,
                    DatumNarudzbe = x.DatumNarudzbe,
                    Dijelovi = dijelovi,
                    Ukupno = x.Ukupno,
                    Zavrsena = x.Zavrsena
                };
                return PartialView("_NarudzbaDetalji", model);
            }

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
            if(_shoppingCart.ShoppingCartItems.Count == 0)
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
                    ukupno=ukupno*0.9;
                }
                order.Ukupno = ukupno;
                order.Zavrsena = false;
                
                var pretraga = _context.osoba.Include(s=>s.Klijent).SingleOrDefault(x => x.Ime == order.Ime && x.Prezime==order.Prezime);
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
                        Grad = _context.grad.Where(g => g.Naziv == order.Grad).SingleOrDefault(),
                        Klijent = new Klijent
                        {
                            DatumRegistracije = order.DatumNarudzbe,
                            BrojNarudzbi = 1
                        }
                    };
                    _context.osoba.Add(nova);
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
                    pretraga.Klijent.BrojNarudzbi++;
                    _context.SaveChanges();
                    _hubContext.Clients.All.SendAsync("displayNotification", "");
                }
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }
        public IActionResult CheckoutComplete()
        {
            _hubContext.Clients.All.SendAsync("displayNotification", "");
            //_hubContext.Clients.All.SendAsync("displayNotification", "");
            ViewBag.CheckoutCompleteMessage = "Hvala Vam na Vasoj narudzbi !";
            return View();
        }
        [HttpGet]
        public IActionResult GetNotification()
        {

            var nove = _context.Orders.Where(x => x.Zavrsena == false);

            int broj = nove.Count();

            return Ok(broj);

        }


    }
}