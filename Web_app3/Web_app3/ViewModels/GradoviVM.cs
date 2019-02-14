using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class GradoviVM
    {

        public List<SelectListItem> gradovi { get; set; }
        public List<SelectListItem> poslovnice { get; set; }
        public List<SelectListItem> Vrsta { get; set; }


    }
}
