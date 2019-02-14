using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoServis.EF;
using AutoServis.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoServis.Models;
using AutoServis.Helper;
using System.Net.Mail;
using System.Net;

namespace AutoServis.Controllers
{
    [Autorizacija(false, false, false, true)]
    public class AdministratorController : Controller
    {
        private MojContext _context;

        public AdministratorController(MojContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upiti()
        {
            var model = _context.KontaktUpiti.Where(x => x.Pregledano == false).ToList();
            return View(model);
        }

        public IActionResult OdgUpiti()
        {
            var model = _context.KontaktUpiti.Where(x=>x.Pregledano==true).ToList();
            return View(model);
        }

        public IActionResult Detalji(int? id)
        {
            var model = _context.KontaktUpiti.Find(id);
            return PartialView(model);
        }



        public IActionResult PosaljiEmail(int id, string email, string poruka)
        {
            var uposlenik = HttpContext.GetLogiraniKorisnik();
            var model = _context.KontaktUpiti.SingleOrDefault(x => x.Id == id);
            model.Pregledano = true;
            _context.SaveChanges();
            string adresa1 = "vlasnikner@gmail.com";


            var fromAddress = new MailAddress(adresa1, "Auto-Kuća NER");
            var toAddress = new MailAddress(email, "Poštovani");
            const string fromPassword = "kamin123kamin";
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
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return RedirectToAction("OdgUpiti");
        }




    }
}