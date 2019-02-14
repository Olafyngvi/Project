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
    public class ModelController : Controller
    {
        private readonly MojContext _context;
        private readonly int PageSize = 6;

        public ModelController(MojContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search, int Page = 1, string sortExpression = "Naziv")
        {
            var qry = _context.model.AsNoTracking()
                   .Include(p => p.marka)
                   .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                qry = qry.Where(x => x.Naziv.Contains(search) ||
                x.marka.Nazvi.Contains(search));
            }

            var model = await PagingList<Model>.CreateAsync(
                                 qry, PageSize, Page, sortExpression, "Naziv");

            model.RouteValue = new RouteValueDictionary {
            { "filter", search}
            };

            return View(model);
        }

        // GET: Model/Create
        public IActionResult Create()
        {
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            return View();
        }
        public IActionResult Dodaj()
        {
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            return PartialView();
        }
        public IActionResult DodajModal()
        {
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            return PartialView();
        }

        // POST: Model/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,Naziv,MarkaID")] Model model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi", model.marka);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dodaj([Bind("ModelId,Naziv,MarkaID")] Model model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi", model.marka);
            return View(model);
        }
        // GET: Model/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model.Include(x=>x.marka).SingleOrDefaultAsync(m => m.ModelId == id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi", model.marka);
            return PartialView(model);
        }

        public async Task<IActionResult> brisi(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model
                .SingleOrDefaultAsync(m => m.ModelId == id);

            if (model == null)
            {
                return NotFound();
            }
            _context.model.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult stavkaBrisi(int? id)
        {
            Model a = _context.model.Find(id);
            _context.model.Remove(a);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Pregled(int id)
        {
            Model m = _context.model.Where(d => d.ModelId == id).Include(o => o.marka).Select(c=>c).FirstOrDefault();
            ModelMarka k = new ModelMarka
            {
                model = m.Naziv,
                marka = m.marka.Nazvi,
                ID = m.ModelId
            };


            return View("Delete", k);
        }


        // POST: Model/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelId,Naziv,MarkaID")] Model model)
        {
            if (id != model.ModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.ModelId))
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
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi", model.marka);
            return View(model);
        }

       
        private bool ModelExists(int id)
        {
            return _context.model.Any(e => e.ModelId == id);
        }
    }
}
