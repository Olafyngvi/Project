using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.Models;

namespace AutoServis.Models
{
    public class DetaljiProdaje
    {
        public int DetaljiProdajeId { get; set; }
        public NacinPlacanja NacinPlacanja { get; set; }
        public int NacinPlacanjaID { get; set; }
        public DateTime DatumProdaje { get; set; }
        public double Cijena { get; set; }
        public VoziloProdaja Vozilo { get; set; }
        public int VoziloProdajaID { get; set; }
        public Klijent Klijent { get; set; }
        public int KlijentID { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int UposlenikID { get; set; }
        


    }
}
