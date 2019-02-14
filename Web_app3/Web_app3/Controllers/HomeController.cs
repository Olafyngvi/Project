using AutoServis.EF;
using AutoServis.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoServis.Controllers
{

    public class HomeController : Controller
    {
        private MojContext _context;
        public HomeController(MojContext db)
        {
            _context = db;
        }


        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult Onama()
        {
            return View("Onama2");
        }

        public IActionResult Kontakt()
        {
            return View();
        }

        public IActionResult Snimi(string ime, string email, string poruka)
        {
            KontaktUpiti kontaktUpiti = new KontaktUpiti
            {
                ImePrezime = ime,
                Email = email,
                Poruka = poruka
            };
            _context.Add(kontaktUpiti);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}