using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoServis.Models;

namespace AutoServis.ViewModels
{
    public class KlijentEdit
    {
        public List<data> lista { get; set; }
        public List <Klijent> klijenti { get; set; }
        public List<SelectListItem> gradovi { get; set; }

        public class data
        {
            

            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Adresa { get; set; }
            public string KorisnickoIme { get; set; }
            public DateTime Datum { get; set; }
            public string Telefon { get; set; }
            public string Grad { get; set; }
            public string Lozinka { get; set; }
            public int BrojNarudzbi { get; set; }

            public int Id { get; set; }
        }
    }
}
