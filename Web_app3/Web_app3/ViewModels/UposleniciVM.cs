using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.Models;

namespace AutoServis.ViewModels
{
    public class UposleniciVM
    {
        public List<data> podaci { get; set; }
        public List<Uposlenik> uposlenici;
       public int vrstaID { get; set; }

        public class data
        {
            public string ime { get; internal set; }
            public string prezime { get; internal set; }
            public string adresa { get; internal set; }
            public string korisnicko { get; internal set; }
            public string lozinka { get; internal set; }
            public string telefon { get; internal set; }
            public string datumzaposljavanja { get; internal set; }
            public string jmbg { get; internal set; }
            public string datumrodjenja { get; internal set; }
            public string poslovnica { get; internal set; }
            public string grad { get; internal set; }
            public string vrsta { get; internal set; }
            public bool neaktivan { get; internal set; }
            public int itemID { get; internal set; }
        }
    }
}
