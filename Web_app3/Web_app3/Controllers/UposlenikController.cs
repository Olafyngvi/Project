using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using AutoServis.ViewModels;
using AutoServis.Helper;

namespace AutoServis.Controllers
{
    [Autorizacija(false, false, false, true)]
    public class UposlenikController : Controller
    {
        private MojContext _context;

        public UposlenikController(MojContext context)
        {
            _context = context;
        }

        // GET: Uposlenik
        public async Task<IActionResult> Index(string pretraga,int? vrstaID)
        {

            if (pretraga !=null || vrstaID!=null)
            {
                
                var uposlenik = from u in _context.uposlenik
                                select u;
                if (!String.IsNullOrEmpty(pretraga))
                {
                    uposlenik = uposlenik.Include(x => x.Osoba)
                        .Include(x=>x.Osoba.Grad)
                        .Include(x=>x.Poslovnica)
                        .Include(x=>x.VrstaUposlenika)
                        .Where(x => x.Osoba.Ime.Contains(pretraga) || x.Osoba.Grad.Naziv.Contains(pretraga));
                }

                if (vrstaID!=null)
                uposlenik = uposlenik.Include(x => x.VrstaUposlenika).Include(x => x.Osoba).Include(x => x.Osoba.Grad).Include(x => x.Poslovnica).Where(x => x.VrstaUposlenika.Id == vrstaID);




                var uVM = new UposleniciVM
                {
                    uposlenici = await uposlenik.ToListAsync()
                };
                return View("Find", uVM);
            }

            ViewData["vrstaID"] = new SelectList(_context.vrstauposlenika, "Id", "Naziv");

            var model = new UposleniciVM
            {
                podaci = _context.uposlenik.Include(x => x.Osoba)
                .Select(y => new UposleniciVM.data
                {
                    itemID=y.Id,
                    ime = y.Osoba.Ime,
                    prezime = y.Osoba.Prezime,
                    adresa = y.Osoba.Adresa,
                    korisnicko = y.Osoba.KorisnickoIme,
                    lozinka = y.Osoba.Lozinka,
                    telefon = y.Osoba.Telefon,
                    datumzaposljavanja = y.DatumZaposljavanja.ToShortDateString(),
                    jmbg = y.JMBG,
                    datumrodjenja = y.DatumRodjenja.ToShortDateString(),
                    poslovnica = y.Poslovnica.Naziv,
                    grad = y.Osoba.Grad.Naziv,
                    vrsta = y.VrstaUposlenika.Naziv,
                    neaktivan=y.Neaktivan
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult checkusername(string username)
        {
            bool result = _context.osoba.ToList().Exists(model => model.KorisnickoIme.Equals(username, StringComparison.CurrentCultureIgnoreCase));
            return Json(result);
        }
        // GET: Uposlenik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Uposlenik a =await _context.uposlenik.FindAsync(id);
            Osoba o =await _context.osoba.FindAsync(a.OsobaId);

            UrediUposlenikVM model = new UrediUposlenikVM
            {
                stavID = a.Id,
                ime = o.Ime,
                prezime = o.Prezime,
                adresa = o.Adresa,
                Koris = o.KorisnickoIme,
                loz = o.Lozinka,
                tel = o.Telefon,
                dz = a.DatumZaposljavanja,
                jmbg = a.JMBG,
                dr = a.DatumRodjenja,
                posl = _context.poslovnica.Where(x => x.Id == a.PoslovnicaId).Select(m => m.Naziv).FirstOrDefault(),
                grad = _context.grad.Where(i => i.Id == o.GradId).Select(m => m.Naziv).FirstOrDefault(),
                vrsta = _context.vrstauposlenika.Where(x => x.Id == a.VrstaUposlenikaId).Select(m => m.Naziv).FirstOrDefault(),
                poslovnice = _context.poslovnica
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList(),
                gradovi = _context.grad
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList(),
                vrsteUposlenika = _context.vrstauposlenika
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList(),



            };

            return PartialView(model);
        }
       
        public IActionResult snimi(int id,string ime,string prezime,string tel,string Kime,string loz,string adresa,int Grad, DateTime datumZaposljavanja,string jmbg, DateTime DatumRodjenja,int vrsta,int poslovnica,bool aktivnost)
        {
            ViewData["vrstaID"] = new SelectList(_context.vrstauposlenika, "Id", "Naziv");
            ViewData["GradID"] = new SelectList(_context.grad, "Id", "Naziv");
            ViewData["PoslovnicaID"] = new SelectList(_context.poslovnica, "Id", "Naziv");
            var pretraga = _context.osoba.SingleOrDefault(x => x.KorisnickoIme == Kime);
            //if (pretraga != null)
            //    return View();
            if (id == 0)
            {

                Osoba a = new Osoba
                {
                    Ime = ime,
                    Prezime = prezime,
                    Adresa = adresa,
                    KorisnickoIme = Kime,
                    Lozinka = loz,
                    Telefon = tel,
                    Grad = _context.grad.Where(x => x.Id == Grad).SingleOrDefault(),
                    Uposlenik = new Uposlenik
                    {
                        DatumZaposljavanja = datumZaposljavanja,
                        DatumRodjenja = DatumRodjenja,
                        Poslovnica = _context.poslovnica.Where(x => x.Id == poslovnica).FirstOrDefault(),
                        JMBG = jmbg,
                        VrstaUposlenika = _context.vrstauposlenika.Where(x => x.Id == vrsta).FirstOrDefault(),
                        Neaktivan=aktivnost

                    }
                };
                _context.osoba.Add(a);
                _context.SaveChanges();
            }

            else
            {
                Uposlenik uposlenik = _context.uposlenik.Find(id);
                Osoba osoba = _context.osoba.Find(uposlenik.OsobaId);



                osoba.Ime = ime;
                osoba.Prezime = prezime;
                osoba.Adresa = adresa;
                osoba.KorisnickoIme = Kime;
                osoba.Lozinka = loz;
                osoba.Telefon = tel;
                osoba.Grad = _context.grad.Where(x => x.Id == Grad).SingleOrDefault();

                uposlenik.DatumZaposljavanja = datumZaposljavanja;
                uposlenik.DatumRodjenja = DatumRodjenja;
                uposlenik.Neaktivan = aktivnost;
                uposlenik.Poslovnica = _context.poslovnica.Where(x => x.Id == poslovnica).FirstOrDefault();
                        uposlenik.JMBG = jmbg;
                uposlenik.VrstaUposlenika = _context.vrstauposlenika.Where(x => x.Id == vrsta).FirstOrDefault();

                _context.SaveChanges();


                }
            return RedirectToAction("Index");
        }

        // GET: Uposlenik/Create
        public IActionResult Create()
        {
            var model = new GradoviVM
            {
                gradovi = _context.grad
                   .Select(x => new SelectListItem
                   {

                       Value = x.Id.ToString(),
                       Text = x.Naziv
                   }).ToList(),

                     poslovnice = _context.poslovnica
               .Select(x => new SelectListItem
               {

                   Value = x.Id.ToString(),
                   Text = x.Naziv
               }).ToList(),

                 Vrsta = _context.vrstauposlenika
               .Select(x => new SelectListItem
               {

                   Value = x.Id.ToString(),
                   Text = x.Naziv
               }).ToList()
            };
            



            return PartialView("DodavanjeOsUp",model);
        }

      
        public IActionResult Uredi(int id)
        {

            Uposlenik a = _context.uposlenik.Find(id);
            Osoba o = _context.osoba.Find(a.OsobaId);

            UrediUposlenikVM model = new UrediUposlenikVM
            {
                stavID = a.Id,
                ime = o.Ime,
                prezime = o.Prezime,
                adresa = o.Adresa,
                Koris = o.KorisnickoIme,
                loz = o.Lozinka,
                tel = o.Telefon,
                dz = a.DatumZaposljavanja,
                jmbg = a.JMBG,
                dr = a.DatumRodjenja,
                aktivnost=a.Neaktivan,
                posl = _context.poslovnica.Where(x=>x.Id==a.PoslovnicaId).Select(m=>m.Naziv).FirstOrDefault(),
                grad = _context.grad.Where(i => i.Id == o.GradId).Select(m => m.Naziv).FirstOrDefault(),
                vrsta = _context.vrstauposlenika.Where(x => x.Id == a.VrstaUposlenikaId).Select(m => m.Naziv).FirstOrDefault(),
                poslovnice = _context.poslovnica
                .Select(x=> new SelectListItem
                {
                    Value=x.Id.ToString(),
                    Text = x.Naziv
                }).ToList(),
                gradovi = _context.grad
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList(),
                vrsteUposlenika = _context.vrstauposlenika
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList(),



            };



            return PartialView(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var k = await _context.uposlenik
                .Include(u => u.Osoba)
                .Include(z=>z.VrstaUposlenika)
                .Include(x=>x.Poslovnica)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (k == null)
            {
                return NotFound();
            }

            return View(k);
        }



        // POST: Uposlenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uposlenik = await _context.uposlenik.SingleOrDefaultAsync(m => m.Id == id);
            if(uposlenik.Neaktivan!=true)
            uposlenik.Neaktivan = true;
            else return RedirectToAction(nameof(Index));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UposlenikExists(int id)
        {
            return _context.uposlenik.Any(e => e.Id == id);
        }
    }
}
