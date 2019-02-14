using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class UrediUposlenikVM
    {
        public string ime { get; set; }
        public string prezime { get; internal set; }
        public string adresa { get; internal set; }
        public string Koris { get; internal set; }
        public string loz { get; internal set; }
        public string tel { get; internal set; }
        public DateTime dz { get; internal set; }
        public string jmbg { get; internal set; }
        public DateTime dr { get; internal set; }
        public string posl { get; internal set; }
        public string grad { get; internal set; }
        public string vrsta { get; internal set; }
        public int stavID { get; internal set; }
        public bool aktivnost { get; internal set; }

        public List<SelectListItem> poslovnice { get; set; }
        public List<SelectListItem> gradovi { get; set; }
        public List<SelectListItem> vrsteUposlenika { get; set; }
    }
}
