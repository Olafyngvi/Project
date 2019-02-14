using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoServis.EF;
using AutoServis.ViewModels;
using Microsoft.EntityFrameworkCore;
using AutoServis.Models;
using Microsoft.AspNetCore.Http;
using AutoServis.Helper;

namespace AutoServis.Controllers
{
    public class AutentifikacijaController : Controller
    {

        private MojContext _contex;

       public AutentifikacijaController(MojContext context)
        {
            _contex = context;
        }
        public IActionResult Index()
        {
            return View(new LoginVM()
            {
                ZapamtiPassword = true,
            });
        }

        public IActionResult Login(LoginVM input)
        {
            Uposlenik korisnik1  = _contex.uposlenik.Include(x=>x.Osoba).Include(x=>x.VrstaUposlenika).SingleOrDefault(x => x.Osoba.KorisnickoIme == input.username && x.Osoba.Lozinka == input.password);
            if (korisnik1 != null && korisnik1.Neaktivan==false)
            {

                HttpContext.SetLogiraniKorisnik(korisnik1);
                
                if (korisnik1.VrstaUposlenikaId == 1)
                {
                    return RedirectToAction("Index", "Uposlenik" );
                }
                if (korisnik1.VrstaUposlenikaId == 2)
                {
                    return RedirectToAction("Pocetna", "MenadžerAutomobila");
                }
                if (korisnik1.VrstaUposlenikaId == 3)
                {
                    return RedirectToAction("Index", "Order");
                }
                if (korisnik1.VrstaUposlenikaId == 4)
                {
                    return RedirectToAction("Index", "Popravka");
                }

            }
            else
            {
                TempData["error_poruka"] = "Pogrešan username ili password";
                return View("Index", input);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Home");
        }
    }
}
