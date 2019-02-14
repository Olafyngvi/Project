using AutoServis.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Components
{
    public class NavMenuViewComponent :ViewComponent
    {
        private MojContext _context;
        public NavMenuViewComponent(MojContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedModel = RouteData?.Values["mod"];
            ViewBag.SelectedMake = RouteData?.Values["mar"];
            string mark = ViewBag.SelectedMake;
            return View(_context.dio
             .Where(y=>y.model.marka.Nazvi==mark)
            .Select(x => x.model.Naziv)           
            .Distinct()
            .OrderBy(x => x));
        }
    }
}
