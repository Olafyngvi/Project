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
    public class PoslovnicaController : Controller
    {
        private readonly MojContext _context;
        private readonly int PageSize = 6;

        public PoslovnicaController(MojContext context)
        {
            _context = context;
        }

        // GET: Poslovnica
        public async Task<IActionResult> Index(string search, int Page = 1, string sortExpression = "Naziv")
        {
            var qry = _context.poslovnica.AsNoTracking()
                .Include(p=>p.Grad)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                qry = qry.Where(x => x.Naziv.Contains(search) ||
                x.Telefon.Contains(search) || x.Grad.Naziv.Contains(search));
            }

            var model = await PagingList<Poslovnica>.CreateAsync(
                                 qry, PageSize, Page, sortExpression, "Naziv");

            model.RouteValue = new RouteValueDictionary {
            { "filter", search}
            };

            return View(model);
        }

        // GET: Poslovnica/Details/5
        public async Task<IActionResult> Details(string naziv)
        {
            if (!string.IsNullOrEmpty(naziv))
            {
                var poslovnica = await _context.poslovnica
                .Include(p => p.Grad)
                .Where(p => p.Naziv == naziv)
                .SingleOrDefaultAsync();
                return PartialView(poslovnica);
            }
            return PartialView();
        }

        // GET: Poslovnica/Create
        public IActionResult Create()
        {
            ViewData["GradId"] = new SelectList(_context.grad, "Id", "Naziv");
            return PartialView();
        }

        // POST: Poslovnica/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Adresa,Telefon,GradId")] Poslovnica poslovnica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poslovnica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradId"] = new SelectList(_context.grad, "Id", "Naziv", poslovnica.GradId);
            return View(poslovnica);
        }

        // GET: Poslovnica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poslovnica = await _context.poslovnica.SingleOrDefaultAsync(m => m.Id == id);
            if (poslovnica == null)
            {
                return NotFound();
            }
            ViewData["GradId"] = new SelectList(_context.grad, "Id", "Naziv", poslovnica.GradId);
            return PartialView(poslovnica);
        }

        // POST: Poslovnica/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Adresa,Telefon,GradId,Zatvorena")] Poslovnica poslovnica)
        {
            if (id != poslovnica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poslovnica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoslovnicaExists(poslovnica.Id))
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
            ViewData["GradId"] = new SelectList(_context.grad, "Id", "Naziv", poslovnica.GradId);
            return View(poslovnica);
        }

        // GET: Poslovnica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poslovnica = await _context.poslovnica
                .Include(p => p.Grad)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (poslovnica == null)
            {
                return NotFound();
            }

            return View(poslovnica);
        }

        // POST: Poslovnica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poslovnica = await _context.poslovnica.SingleOrDefaultAsync(m => m.Id == id);
            poslovnica.Zatvorena = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Obrisi(int id)
        {
            var poslovnica = await _context.poslovnica.SingleOrDefaultAsync(m => m.Id == id);
            poslovnica.Zatvorena = true;
            _context.Update(poslovnica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoslovnicaExists(int id)
        {
            return _context.poslovnica.Any(e => e.Id == id);
        }
    }
}
