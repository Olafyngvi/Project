using AutoServis.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class VoziloProdajaVM
    {
        public List<Auta> redovi { get; set; }
        public List<SelectListItem> transmisija { get; set; }
        public List<SelectListItem> brojVrata { get; set; }
        public List<SelectListItem> tipVozila { get; set; }
        public List<SelectListItem> oprema { get; set; }
        public List<SelectListItem> gorivo { get; set; }
        public List<SelectListItem> model { get; set; }
        public List<VoziloProdaja> vozilo;
        public int brojVozila { get; set; }
        public int brojVrataID { get; set; }
        public int TransmisijaID { get; set; }
        public int GorivoID { get; set; }
        public int MarkaID { get; set; }
        public int ModelID { get; set; }
        public int TipVoziilaID { get; set; }
        public int OpremaID { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }






        public class Auta
        {
            public int VoziloProdajaID { get; set; }
            public DateTime DatumProizvodnje { get; set; }
            public string Marka { get; set; }
            public string Kilometraza { get; set; }
            public string Kubikaza { get; set; }
            public string SnagaMotora { get; set; }
            public double Cijena { get; set; }
            public bool isDeleted { get; set; }
            public string Sifra { get; set; }
            public string transmisija { get; set; }
            public string brojVrata { get; set; }
            public string tipVozila { get; set; }
            public string oprema { get; set; }
            public string gorivo { get; set; }
            public string model { get; set; }

            


        }
    }
}
