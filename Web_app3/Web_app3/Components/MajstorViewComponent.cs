using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Components
{
    public class MajstorViewComponent :ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
