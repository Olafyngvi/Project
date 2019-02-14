using AutoServis.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Components
{
    public class NavMenuMakeViewComponent : ViewComponent
    {
        private MojContext _context;
        public NavMenuMakeViewComponent(MojContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedMake = RouteData?.Values["mar"];
            return View(_context.dio
             .Include(y=>y.model.marka)
            .Select(x => x.model.marka.Nazvi)
            .Distinct()
            .OrderBy(x => x));
        }
    }
}
