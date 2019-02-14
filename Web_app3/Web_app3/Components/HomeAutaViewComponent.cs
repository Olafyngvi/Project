using AutoServis.EF;
using AutoServis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Components
{
    public class HomeAutaViewComponent:ViewComponent
    {
        private MojContext _context;
        public HomeAutaViewComponent(MojContext db)
        {
            _context = db;      
        }
        public IViewComponentResult Invoke()
        {

            
               var voziloProdaja=_context.voziloprodaja
                .Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(x => x.isDeleted == false).OrderByDescending(x=>x.Cijena).Take(4);

            return View(voziloProdaja);
        }
    }
}
