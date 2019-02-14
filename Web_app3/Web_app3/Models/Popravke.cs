using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.Models;

namespace AutoServis.Models
{
    public class Popravke
    {
        public int PopravkeId { get; set; }
        public string OpisKvara { get; set; }
        public string BrojPopravke { get; set; }
        public DateTime DatumPopravke { get; set; }
        public double CijenaPopravke { get; set; }
        public Poslovnica Poslovnica { get; set; }
        public int PoslovnicaID { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int UposlenikID { get; set; }

    }
}
