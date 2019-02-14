using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;
using AutoServis.Helper;

namespace AutoServis.Controllers
{
    [Autorizacija(false,false,true,false)]
    public class MarkaController : Controller
    {
        private readonly MojContext _context;
        private readonly int PageSize = 6;

        public MarkaController(MojContext context)
        {
            _context = context;
        }

        // GET: Marka
        public async Task<IActionResult> _Index()
        {
            return View(await _context.marka.ToListAsync());
        }
        public async Task<IActionResult> Index(string search, int Page = 1, string sortExpression = "Nazvi")
        {
            var qry = _context.marka.AsNoTracking()
                   .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                qry = qry.Where(x => x.Nazvi.Contains(search));
            }

            var model = await PagingList<Marka>.CreateAsync(
                                 qry, PageSize, Page, sortExpression, "Nazvi");

            model.RouteValue = new RouteValueDictionary {
            { "filter", search}
            };

            return View(model);
        }

        // GET: Marka/Details/7
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.marka
                .SingleOrDefaultAsync(m => m.MarkaId == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // GET: Marka/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Dodaj()
        {
            return PartialView();
        }
        public IActionResult DodajModal()
        {
            return PartialView();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dodaj([Bind("MarkaId,Nazvi")] Marka marka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marka);
        }

        // POST: Marka/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarkaId,Nazvi")] Marka marka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marka);
        }

        // GET: Marka/Edit/7
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.marka.SingleOrDefaultAsync(m => m.MarkaId == id);
            if (marka == null)
            {
                return NotFound();
            }
            return PartialView(marka);
        }

        // POST: Marka/Edit/7

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarkaId,Nazvi")] Marka marka)
        {
            if (id != marka.MarkaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkaExists(marka.MarkaId))
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
            return View(marka);
        }

        //GET: Marka/Delete/7
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.marka
                .SingleOrDefaultAsync(m => m.MarkaId == id);
            if (marka == null)
            {
                return NotFound();
            }
            _context.marka.Remove(marka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkaExists(int id)
        {
            return _context.marka.Any(e => e.MarkaId == id);
        }
    }
}
