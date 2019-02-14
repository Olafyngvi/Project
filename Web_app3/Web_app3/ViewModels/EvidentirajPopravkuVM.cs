using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoServis.ViewModels
{
    public class EvidentirajPopravkuVM
    {
        public List<SelectListItem> poslovnice { get; set; }
        public List<SelectListItem> Uposlenici { get; set; }
    }
}
