using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using AutoServis.Helper;

namespace AutoServis.Controllers
{
    [Autorizacija(false, false, true, false)]
    public class DioKategorijaController : Controller
    {
        private readonly MojContext _context;

        public DioKategorijaController(MojContext context)
        {
            _context = context;
        }

        // GET: DioKategorija
        public async Task<IActionResult> Index()
        {
            return View(await _context.DioKategorija.ToListAsync());
        }

        // GET: DioKategorija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dioKategorija = await _context.DioKategorija
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dioKategorija == null)
            {
                return NotFound();
            }

            return View(dioKategorija);
        }

        // GET: DioKategorija/Create
        public IActionResult Create()
        {
            return PartialView();
        }
        public IActionResult DodajModal()
        {
            return PartialView();
        }


        // POST: DioKategorija/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DioKategorija dioKategorija,List<IFormFile> Slika)
        {
            foreach(var item in Slika)
            {
                if(item.Length>0)
                {
                    using(var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        dioKategorija.Slika = stream.ToArray();
                    }
                }
            } 
            if (ModelState.IsValid)
            {
                _context.Add(dioKategorija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dioKategorija);
        }

        // GET: DioKategorija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dioKategorija = await _context.DioKategorija.SingleOrDefaultAsync(m => m.Id == id);
            if (dioKategorija == null)
            {
                return NotFound();
            }
            return PartialView(dioKategorija);
        }

        // POST: DioKategorija/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Slika")] DioKategorija dioKategorija)
        {
            if (id != dioKategorija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dioKategorija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DioKategorijaExists(dioKategorija.Id))
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
            return View(dioKategorija);
        }

        // GET: DioKategorija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dioKategorija = await _context.DioKategorija
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dioKategorija == null)
            {
                return NotFound();
            }

            return View(dioKategorija);
        }

        // POST: DioKategorija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dioKategorija = await _context.DioKategorija.SingleOrDefaultAsync(m => m.Id == id);
            _context.DioKategorija.Remove(dioKategorija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DioKategorijaExists(int id)
        {
            return _context.DioKategorija.Any(e => e.Id == id);
        }
    }
}
