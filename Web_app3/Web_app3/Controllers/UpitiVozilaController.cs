using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using AutoServis.Helper;
using System.Net;
using System.Net.Mail;

namespace AutoServis.Controllers
{

    public class UpitiVozilaController : Controller
    {
        private readonly MojContext _context;

        public UpitiVozilaController(MojContext context)
        {
            _context = context;
        }

       [HttpPost]
       [ValidateAntiForgeryToken]
        public IActionResult Create(string ime, string text,string email, int id)
        {
            var auto = _context.voziloprodaja.SingleOrDefault(x => x.VoziloProdajaID == id);
            string sifra = auto.SifraAutomobila;
            UpitiVozila upitiVozila = new UpitiVozila
            {
                ImeiPrezime = ime,
                Email = email,
                Text = text,
                SifraAutomobila = sifra,
                DatumVrijem = DateTime.Now,
                Pregledano = false
            };
            _context.upitivozila.Add(upitiVozila);
            _context.SaveChanges();
            return RedirectToAction("Details2/"+id, "AutomobiliKlijent",new { area= "KlijentModul" });

        }

        
      



    }  
}
