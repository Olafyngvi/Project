using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class VoziloProdaja
    {
        public int VoziloProdajaID { get; set; }
        public Transmisija Transmisija { get; set; }
        public int TransmisijaID { get; set; }
        public DateTime DatumProizvodnje { get; set; }
        public string Kilometraza { get; set; }
        public BrojVrata BrojVrata { get; set; }
        public int BrojVrataID { get; set; }
        public TipVozila TipVozila { get; set; }
        public int TipVozilaID { get; set; }
        public string Kubikaza { get; set; }
        public string SnagaMotora { get; set; }
        public double Cijena { get; set; }
        public Oprema Oprema { get; set; }
        public int OpremaID { get; set; }
        public bool isDeleted { get; set; }
        public Gorivo Gorivo { get; set; }
        public int GorivoID { get; set; }
        public Model Model { get; set; }
        public int ModelID { get; set; }
        public string SifraAutomobila { get; set; }
        public string Slika { get; set; }
        public int brojPregleda { get; set; }
        
    }
}
