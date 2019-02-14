using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.EF;
using AutoServis.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoServis.Controllers
{
    public class DijeloviController : Controller
    {
        private int PageSize=8;
        private readonly MojContext _context;
        public DijeloviController(MojContext context)
        {
            _context = context;
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
            else if (!string.IsNullOrWhiteSpace(mod))
            {
                var model = new DioListVM
                {
                    Dijelovi = _context.dio
                                        .Where(p => p.model.Naziv == mod && p.KategorijaId == id)
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
    }
}