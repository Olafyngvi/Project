﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Components
{
    public class MenadzerDijeloviBar:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
