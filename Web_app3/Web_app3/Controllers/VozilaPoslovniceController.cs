using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;
using AutoServis.Helper;

namespace AutoServis.Controllers
{
    [Autorizacija(false, false, false, true)]
    public class VozilaPoslovniceController : Controller
    {
        private readonly MojContext _context;

        public VozilaPoslovniceController(MojContext context)
        {
            _context = context;
        }

        // GET: VozilaPoslovnice
        public async Task<IActionResult> Index(string search, int Page = 1, string sortExpression = "DatumUvoza")
        {
            var qry = _context.vozilaposlovnice.AsNoTracking()
                    .Include(x => x.Poslovnica)
                    .Include(p => p.VoziloProdaja)
                    .Include(s => s.VoziloProdaja.Model)
                    .Include(x => x.VoziloProdaja)
                    .Include(x => x.VoziloProdaja.Model.marka)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                qry = qry.Where(x => x.DatumUvoza.Year.ToString() == search || x.DatumUvoza.Month.ToString() == search
                || x.Poslovnica.Naziv.Contains(search)
                || x.VoziloProdaja.Model.Naziv.Contains(search) || x.VoziloProdaja.Model.marka.Nazvi.Contains(search));
            }

            var model = await PagingList<VozilaPoslovnice>.CreateAsync(
                                 qry, 10, Page, sortExpression, "DatumUvoza");

            model.RouteValue = new RouteValueDictionary {
            { "filter", search}
            };

            return View(model);
        }
        // GET: VozilaPoslovnice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilaPoslovnice = await _context.vozilaposlovnice
                .Include(v => v.Poslovnica)
                .Include(v => v.VoziloProdaja)
                .SingleOrDefaultAsync(m => m.VozilaPoslovniceId == id);
            if (vozilaPoslovnice == null)
            {
                return NotFound();
            }

            return View(vozilaPoslovnice);
        }

        // GET: VozilaPoslovnice/Create
        public IActionResult Create()
        {
            ViewData["PoslovnicaID"] = new SelectList(_context.poslovnica, "Id", "Naziv");
            ViewData["VoziloProdajaID"] = new SelectList(_context.voziloprodaja, "VoziloProdajaID", "SifraAutomobila");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VozilaPoslovniceId,DatumUvoza,PoslovnicaID,VoziloProdajaID")] VozilaPoslovnice vozilaPoslovnice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vozilaPoslovnice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoslovnicaID"] = new SelectList(_context.poslovnica, "Id", "Naziv", vozilaPoslovnice.Poslovnica);
            ViewData["VoziloProdajaID"] = new SelectList(_context.voziloprodaja, "VoziloProdajaID", "SifraAutomobila", vozilaPoslovnice.VoziloProdaja);
            return View(vozilaPoslovnice);
        }

        // GET: VozilaPoslovnice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilaPoslovnice = await _context.vozilaposlovnice.SingleOrDefaultAsync(m => m.VozilaPoslovniceId == id);
            if (vozilaPoslovnice == null)
            {
                return NotFound();
            }
            ViewData["PoslovnicaID"] = new SelectList(_context.poslovnica, "Id", "Naziv", vozilaPoslovnice.Poslovnica);
            ViewData["VoziloProdajaID"] = new SelectList(_context.voziloprodaja, "VoziloProdajaID", "SifraAutomobila", vozilaPoslovnice.VoziloProdaja);
            return View(vozilaPoslovnice);
        }

        // POST: VozilaPoslovnice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VozilaPoslovniceId,DatumUvoza,PoslovnicaID,VoziloProdajaID")] VozilaPoslovnice vozilaPoslovnice)
        {
            if (id != vozilaPoslovnice.VozilaPoslovniceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vozilaPoslovnice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VozilaPoslovniceExists(vozilaPoslovnice.VozilaPoslovniceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoslovnicaID"] = new SelectList(_context.poslovnica, "Id", "Naziv", vozilaPoslovnice.PoslovnicaID);
            ViewData["VoziloProdajaID"] = new SelectList(_context.voziloprodaja, "VoziloProdajaID", "SifraAutomobila", vozilaPoslovnice.VoziloProdaja);
            return View(vozilaPoslovnice);
        }

        // GET: VozilaPoslovnice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilaPoslovnice = await _context.vozilaposlovnice
                .Include(v => v.Poslovnica)
                .Include(v => v.VoziloProdaja)
                .SingleOrDefaultAsync(m => m.VozilaPoslovniceId == id);
            if (vozilaPoslovnice == null)
            {
                return NotFound();
            }

            return View(vozilaPoslovnice);
        }

        // POST: VozilaPoslovnice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vozilaPoslovnice = await _context.vozilaposlovnice.SingleOrDefaultAsync(m => m.VozilaPoslovniceId == id);
            _context.vozilaposlovnice.Remove(vozilaPoslovnice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VozilaPoslovniceExists(int id)
        {
            return _context.vozilaposlovnice.Any(e => e.VozilaPoslovniceId == id);
        }
    }
}
