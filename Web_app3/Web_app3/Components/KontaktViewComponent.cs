using AutoServis.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Components
{
    public class KontaktViewComponent:ViewComponent
    {

        private MojContext _context;

        public KontaktViewComponent(MojContext db)
        {
            _context = db;
        }

        public IViewComponentResult Invoke()
        {
            var poslovnice = _context.poslovnica.Where(x=>x.Zatvorena==false).Include(x=>x.Grad).ToList();
            return View(poslovnice);
        } 
    }
}
