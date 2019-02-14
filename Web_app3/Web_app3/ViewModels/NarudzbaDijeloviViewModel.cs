using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class NarudzbaDijeloviViewModel
    {
        public int NarudzbaId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string ZipCode { get; set; }
        public string Drzava { get; set; }
        public string BrojTelefona { get; set; }
        public int BrojNarudzbi { get; set; }
        public string Email { get; set; }
        public double Ukupno { get; set; }
        public DateTime DatumNarudzbe { get; set; }
        public bool Zavrsena { get; set; }
        public List<OrderDetails> Dijelovi { get; set; }
    }
}
