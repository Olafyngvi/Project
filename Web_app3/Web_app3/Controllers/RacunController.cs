using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using AutoServis.ViewModels;
using AutoServis.Helper;

namespace AutoServis.Controllers
{

    [Autorizacija(false, true, false, false)]
    public class RacunController : Controller
    {
        private readonly MojContext _context;

   

        public RacunController(MojContext context)
        {
            _context = context;
        }


        

        // GET: Racun/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            InformacijeRacuna model = new InformacijeRacuna
            {
                OpisKvara = _context.stavkeracuna.Where(v => v.RacunID == id).Include(p => p.Popravke).Select(x => x.Popravke.OpisKvara).FirstOrDefault(),
                suma = _context.racun.Where(x => x.RacunId == id).Select(v => v.Ukupno).FirstOrDefault(),
                racunBroj = _context.racun.Where(x => x.RacunId == id).Select(v => v.BrojRacuna).FirstOrDefault(),
                DatumPopravke = _context.racun.Where(x => x.RacunId == id).Select(v => v.Datum).FirstOrDefault(),
                Poslovnica = _context.stavkeracuna.Where(v => v.RacunID == id).Include(p => p.Popravke).Select(x => x.Popravke.Poslovnica.Naziv).FirstOrDefault(),
                UposlenikPrezime = _context.stavkeracuna.Where(v => v.RacunID == id).Include(p => p.Popravke).Include(c => c.Popravke.Uposlenik).Include(c => c.Popravke.Uposlenik.Osoba).Select(x => x.Popravke.Uposlenik.Osoba.Prezime).FirstOrDefault(),
                Uposlenik = _context.stavkeracuna.Where(v => v.RacunID == id).Include(p => p.Popravke).Include(c => c.Popravke.Uposlenik).Include(c => c.Popravke.Uposlenik.Osoba).Select(x => x.Popravke.Uposlenik.Osoba.Ime).FirstOrDefault(),
                BrojPopravke = _context.stavkeracuna.Where(v => v.RacunID == id).Include(p => p.Popravke).Select(x => x.Popravke.BrojPopravke).FirstOrDefault(),
                ListaDijelova = _context.stavkeracuna
                 .Where(x => x.RacunID == id)
                 .Include(d => d.Dio)
                 .Select(v => new InformacijeRacuna.utrosak
                 {
                     UtroseniDio = v.Dio.Naziv,
                     kolicina = v.Kolicina,
                     cijena = v.Cijena,
                     
                 }).ToList(),

               
                cijeaPop = _context.stavkeracuna.Where(v => v.RacunID == id).Include(p => p.Popravke).Select(x => x.Popravke.CijenaPopravke).FirstOrDefault(),
            };

            return View(model);
        }


        public async Task<IActionResult> RacuniPregled()
        {
            var mojContext = _context.racun;
            return View("RacunPregled", await mojContext.ToListAsync());
        }


        public IActionResult SpremiRacun(int RacunID, DateTime DatumPop, string brRacuna, double UkupnoSve)
        {
            Racun NovaStavka = _context.racun.Where(v => v.RacunId == RacunID).Select(x => x).FirstOrDefault();

            NovaStavka.BrojRacuna = brRacuna;
            NovaStavka.Datum = DatumPop;
            NovaStavka.Ukupno = UkupnoSve;
            NovaStavka.Online = false;
            NovaStavka.NacinPlacanjaID = 1;

            _context.Update(NovaStavka);
            _context.SaveChanges();

            return RedirectToAction("RacuniPregled");
        }



        public IActionResult PretragaRacuna(string brRacuna)
        {
            string br = brRacuna.Substring(2, 4);

            NadjenaPopravkaVM model = new NadjenaPopravkaVM
            {
                popravkaId = _context.racun.Where(x => x.BrojRacuna == brRacuna).Select(v => v.RacunId).FirstOrDefault(),
                brojPopravke = _context.racun.Where(x => x.BrojRacuna == brRacuna).Select(v => v.BrojRacuna).FirstOrDefault(),
                datumPopravke = _context.racun.Where(x => x.BrojRacuna == brRacuna).Select(v => v.Datum).FirstOrDefault()
            };
            return PartialView("_pretragaRacuna", model);
        }


      
    }
}
