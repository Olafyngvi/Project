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
    [Autorizacija(true, false, false, false)]
    public class UpitiController : Controller
    {
        private readonly MojContext _context;

        public UpitiController(MojContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return PartialView(await _context.upitivozila.Where(x => x.Pregledano == false).ToListAsync());
        }

        public IActionResult Procitano(int? id)
        {
            var model = _context.upitivozila.Find(id);
            model.Pregledano = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult OU()
        {
            return PartialView("OdgovoreniUpiti", _context.upitivozila.Where(x => x.Pregledano == true).ToList());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upitiVozila = await _context.upitivozila
                .SingleOrDefaultAsync(m => m.ID == id);
            if (upitiVozila == null)
            {
                return NotFound();
            }

            return PartialView(upitiVozila);
        }


       


        public IActionResult Odgovori(int? id)
        {
            var model = _context.upitivozila.SingleOrDefault(x => x.ID == id);

            return View(model);

        }


        public IActionResult PosaljiEmail(int id, string email, string poruka)
        {
            var uposlenik = HttpContext.GetLogiraniKorisnik();
            var model = _context.upitivozila.SingleOrDefault(x => x.ID == id);
            if (model != null) { model.Pregledano = true; }
            _context.SaveChanges();
            string adresa1 = "menadzerprodajeNER@gmail.com";


            var fromAddress = new MailAddress(adresa1, "Auto-Kuća NER");
            var toAddress = new MailAddress(email, "Poštovani");
            const string fromPassword = "kamin123kamin!";
            const string subject = "AutoKucaNER-UPIT";
            string body = "Poštovani, \n" + poruka + "\nLijep pozdrav, " + uposlenik.Osoba.Ime + " " + uposlenik.Osoba.Prezime;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            smtp.Send(message);

            return RedirectToAction(nameof(Index));
        }

    }
}