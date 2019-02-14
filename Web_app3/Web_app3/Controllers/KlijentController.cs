using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using AutoServis.ViewModels;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;
using AutoServis.Helper;

namespace AutoServis.Controllers
{
    [Autorizacija(false, false, true, false)]
    public class KlijentController : Controller
    {
        private readonly MojContext _context;
        private readonly int PageSize = 6;

        public KlijentController(MojContext context)
        {
            _context = context;
        }

        // GET: Klijent
        public async Task<IActionResult> Index(string search, int Page = 1, string sortExpression = "Osoba.Ime")
        {
            var qry = _context.klijent.AsNoTracking()
                    .Include(p => p.Osoba)
                    .Include(s=>s.Osoba.Grad)
                    .AsQueryable();
            ViewData["Ukupno"] = _context.klijent.Count();
            if (!string.IsNullOrWhiteSpace(search))
            {
                qry = qry.Where(x => x.Osoba.Ime.Contains(search) ||
                x.Osoba.Prezime.Contains(search) ||
                x.Osoba.Grad.Naziv.Contains(search));
            }

            var model = await PagingList<Klijent>.CreateAsync(
                                 qry, PageSize, Page, sortExpression, "Ime");

            model.RouteValue = new RouteValueDictionary {
            { "filter", search}
            };

            return View(model);
        }

        // GET: Klijent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            KlijentEdit model = new KlijentEdit
            {
                lista = await _context.osoba
              .Where(c => c.Klijent.Id == id)
              .Include(k => k.Klijent)
              .Include(g => g.Grad)
              .Select(x =>
              new KlijentEdit.data
              {
                  Ime = x.Ime,
                  Prezime = x.Prezime,
                  Adresa = x.Adresa,
                  KorisnickoIme = x.KorisnickoIme,
                  Datum = x.Klijent.DatumRegistracije,
                  BrojNarudzbi=x.Klijent.BrojNarudzbi,
                  Telefon = x.Telefon,
                  Grad = x.Grad.Naziv,
                  Lozinka = x.Lozinka,
                  Id = x.Klijent.Id

              }).ToListAsync(),
                gradovi = await _context.grad
              .Select(g => new SelectListItem
              {
                  Value = g.Id.ToString(),
                  Text = g.Naziv

              }).ToListAsync()
            };
            return View(model);
        }
        [ValidateAntiForgeryToken]
        public IActionResult snimi(string ime, string prezime, string tel, string Kime, string loz, string adresa, int Grad)
        {

            Osoba a = new Osoba
            {

                Ime = ime,
                Prezime = prezime,
                Adresa = adresa,
                KorisnickoIme = Kime,
                Lozinka = loz,
                Telefon = tel,
                Grad = _context.grad.Where(x => x.Id == Grad).FirstOrDefault(),
                Klijent = new Klijent
                {
                    DatumRegistracije = DateTime.Now,
                    BrojNarudzbi = 0
                }

            };
            _context.osoba.Add(a);
            _context.SaveChanges();

            return  RedirectToAction("Index");
        }

      

        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,string ime, string prezime, string tel, string Kime, string loz, string adresa, int Grad)
        {
            Klijent a = _context.klijent.Find(id);
            Osoba o = _context.osoba.Find(a.OsobaId);


            o.Ime = ime;
            o.Prezime = prezime;
            o.Adresa = adresa;
            o.KorisnickoIme = Kime;
            o.Lozinka = loz;
            o.Telefon = tel;
            o.Grad = _context.grad.Where(x => x.Id == Grad).FirstOrDefault();
       
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        // GET: Klijent/Create
        public IActionResult Create()
        {
            var model = new GradoviVM
            {
                gradovi = _context.grad
                  .Select(x => new SelectListItem
                  {

                      Value = x.Id.ToString(),
                      Text = x.Naziv
                  }).ToList()
            };

            return View("DodavanjeKlijenta",model);
        }

        // POST: Klijent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatumRegistracije,OsobaId")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klijent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OsobaId"] = new SelectList(_context.osoba, "Id", "Id", klijent.OsobaId);
            return View(klijent);
        }
        public IActionResult Edit(int id)
        {
          
            KlijentEdit model = new KlijentEdit
            {
                lista = _context.osoba
                .Where(c => c.Klijent.Id == id)
                .Include(k => k.Klijent)
                .Include(g => g.Grad)
                .Select(x =>
                new KlijentEdit.data
                {
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    KorisnickoIme = x.KorisnickoIme,
                    Datum = x.Klijent.DatumRegistracije,
                    BrojNarudzbi=x.Klijent.BrojNarudzbi,
                    Telefon = x.Telefon,
                    Grad = x.Grad.Naziv,
                    Lozinka = x.Lozinka,
                    Id = x.Klijent.Id
                
                }).ToList(),
                gradovi = _context.grad
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Naziv

                }).ToList()
            };
            return View("Edit",model);

        }

        // GET: Klijent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var k = await _context.klijent
                .Include(u => u.Osoba)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (k == null)
            {
                return NotFound();
            }

            return View(k);
        }

        //POST: Klijent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klijent = await _context.klijent.SingleOrDefaultAsync(m => m.Id == id);
            
            var o = await _context.osoba.SingleOrDefaultAsync(n => n.Id == klijent.OsobaId);
            _context.klijent.Remove(klijent);
            _context.osoba.Remove(o);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlijentExists(int id)
        {
            return _context.klijent.Any(e => e.Id == id);
        }
    }
}
