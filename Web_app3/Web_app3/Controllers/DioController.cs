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
    public class DioController : Controller
    {
        private MojContext _context;
        private int PageSize = 8;

        public DioController(MojContext context)
        {
            _context = context;
        }

        // GET: Dio
        
        public async Task<IActionResult> Index(string search, int Page = 1, string sortExpression = "Naziv")
        {
            var qry = _context.dio.AsNoTracking()
                    .Include(x=>x.Kategorija)
                    .Include(p => p.model)
                    .Include(s=>s.model.marka)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                qry = qry.Where(x => x.Naziv.Contains(search) ||
                x.model.Naziv.Contains(search) ||
                x.Sifra.Contains(search) ||
                x.model.marka.Nazvi.Contains(search)
                ||x.Kategorija.Naziv.Contains(search));
            }
            qry = qry.Where(x => x.IsDeleted == false);
            var model = await PagingList<Dio>.CreateAsync(
                                 qry, 10, Page, sortExpression, "Naziv");

            model.RouteValue = new RouteValueDictionary {
            { "filter", search}
            };

            return View(model);
        }

        public ViewResult List(string mod, int? id, int productPage = 1)
        {
            if (id == null)
            {
                var model = new DioListVM
                {
                    Dijelovi = _context.dio
                        .Where(p => mod == null || p.model.Naziv == mod)
                        .Include(x => x.Kategorija)
                        .Include(g => g.model)
                        .Include(f => f.model.marka)
                        .OrderBy(p => p.DioId)
                        .Skip((productPage - 1) * PageSize)
                        .Take(PageSize),
                    Paging = new Paging
                    {
                        TrenutnaStranica = productPage,
                        ItemaPoStranici = PageSize,
                        UkupnoItema = mod == null ?
                        _context.dio.Count() :
                        _context.dio.Where(e =>
                        e.model.Naziv == mod).Count()
                    },
                    TrenutniModel = mod,
                    kategorije = _context.DioKategorija.ToList()
                };
                return View(model);
            }
            else if(!string.IsNullOrWhiteSpace(mod))
            {
                var model = new DioListVM
                {
                                        Dijelovi = _context.dio
                                        .Where(p=> p.model.Naziv == mod && p.KategorijaId==id)
                                        .Include(x => x.Kategorija)
                                        .Include(g => g.model)
                                        .Include(f => f.model.marka)
                                        .OrderBy(p => p.DioId)
                                        .Skip((productPage - 1) * PageSize)
                                        .Take(PageSize),
                    Paging = new Paging
                    {
                        TrenutnaStranica = productPage,
                        ItemaPoStranici = PageSize,
                        UkupnoItema = mod == null ?
                                        _context.dio.Count() :
                                        _context.dio.Where(e =>
                                        e.model.Naziv == mod).Count()
                    },
                    TrenutniModel = mod,
                    kategorije = _context.DioKategorija.ToList()
                };
                return View(model);
            }
            else
            {
                var model = new DioListVM
                {
                    Dijelovi = _context.dio
                                        .Where(p => p.KategorijaId == id)
                                        .Include(x => x.Kategorija)
                                        .Include(g => g.model)
                                        .Include(f => f.model.marka)
                                        .OrderBy(p => p.DioId)
                                        .Skip((productPage - 1) * PageSize)
                                        .Take(PageSize),
                    Paging = new Paging
                    {
                        TrenutnaStranica = productPage,
                        ItemaPoStranici = PageSize,
                        UkupnoItema = mod == null ?
                                        _context.dio.Count() :
                                        _context.dio.Where(e =>
                                        e.model.Naziv == mod).Count()
                    },
                    TrenutniModel = mod,
                    kategorije = _context.DioKategorija.ToList()
                };
                return View(model);
            }
        }
        // GET: Dio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dio = await _context.dio
                .Include(d => d.model)
                .SingleOrDefaultAsync(m => m.DioId == id);
            if (dio == null)
            {
                return NotFound();
            }

            return View(dio);
        }

        // GET: Dio/Create
        public IActionResult Create()
        {
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv");
            ViewData["KategorijaID"] = new SelectList(_context.DioKategorija, "Id", "Naziv");
            return PartialView();
        }
        public IActionResult CreateModal()
        {
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv");
            ViewData["KategorijaID"] = new SelectList(_context.DioKategorija, "Id", "Naziv");
            return PartialView();
        }

        // POST: Dio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DioId,Naziv,Cijena,Sifra,KategorijaId,ModelID")] Dio dio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "ModelId", dio.ModelID);
            ViewData["KategorijaID"] = new SelectList(_context.DioKategorija, "Id", "Naziv", dio.KategorijaId);
            return View(dio);
        }

        // GET: Dio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dio = await _context.dio.SingleOrDefaultAsync(m => m.DioId == id);
            if (dio == null)
            {
                return NotFound();
            }
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv", dio.ModelID);
            ViewData["KategorijaID"] = new SelectList(_context.DioKategorija, "Id", "Naziv", dio.KategorijaId);
            return PartialView(dio);
        }

        // POST: Dio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DioId,Naziv,Cijena,Sifra,KategorijaId,ModelID")] Dio dio)
        {
            if (id != dio.DioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DioExists(dio.DioId))
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
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "ModelId", dio.ModelID);
            ViewData["DioKategorijaID"] = new SelectList(_context.DioKategorija, "KategorijaId", "Naziv", dio.KategorijaId);
            return View(dio);
        }

        // GET: Dio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dio = await _context.dio
                .Include(d => d.model)
                .SingleOrDefaultAsync(m => m.DioId == id);
            if (dio == null)
            {
                return NotFound();
            }
            dio.IsDeleted = true;
            _context.dio.Update(dio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DioExists(int id)
        {
            return _context.dio.Any(e => e.DioId == id);
        }
    }
}
