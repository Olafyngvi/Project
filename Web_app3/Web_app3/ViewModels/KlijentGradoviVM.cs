using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.Models;

namespace AutoServis.ViewModels
{
    public class KlijentGradoviVMM
    {
        public List<dat> redovi { get; set; }
        public List<Klijent> Klijenti;



        public class dat
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Adresa { get; set; }
            public string KorisnickoIme { get; set; }
            public DateTime Datum { get; set; }
            public string Telefon { get; set; }
            public string Grad { get; set; }
            public string Lozinka { get; set; }

            public int stavid { get; set; }
        }
    }
}

