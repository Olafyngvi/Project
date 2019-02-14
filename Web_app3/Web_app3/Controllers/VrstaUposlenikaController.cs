using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using AutoServis.Helper;

namespace AutoServis.Controllers
{
    [Autorizacija(false, false, false, true)]
    public class VrstaUposlenikaController : Controller
    {
        private readonly MojContext _context;

        public VrstaUposlenikaController(MojContext context)
        {
            _context = context;
        }

        // GET: VrstaUposlenika
        public async Task<IActionResult> Index()
        {
            return View(await _context.vrstauposlenika.ToListAsync());
        }

        // GET: VrstaUposlenika/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaUposlenika = await _context.vrstauposlenika
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaUposlenika == null)
            {
                return NotFound();
            }

            return View(vrstaUposlenika);
        }

        // GET: VrstaUposlenika/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VrstaUposlenika/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv")] VrstaUposlenika vrstaUposlenika)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vrstaUposlenika);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vrstaUposlenika);
        }

        // GET: VrstaUposlenika/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaUposlenika = await _context.vrstauposlenika.SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaUposlenika == null)
            {
                return NotFound();
            }
            return View(vrstaUposlenika);
        }

        // POST: VrstaUposlenika/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv")] VrstaUposlenika vrstaUposlenika)
        {
            if (id != vrstaUposlenika.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vrstaUposlenika);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VrstaUposlenikaExists(vrstaUposlenika.Id))
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
            return View(vrstaUposlenika);
        }

        // GET: VrstaUposlenika/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaUposlenika = await _context.vrstauposlenika
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaUposlenika == null)
            {
                return NotFound();
            }

            return View(vrstaUposlenika);
        }

        // POST: VrstaUposlenika/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vrstaUposlenika = await _context.vrstauposlenika.SingleOrDefaultAsync(m => m.Id == id);
            _context.vrstauposlenika.Remove(vrstaUposlenika);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VrstaUposlenikaExists(int id)
        {
            return _context.vrstauposlenika.Any(e => e.Id == id);
        }
    }
}
