using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class PripremaRacunaVM
    {
        public List<Data> lista { get; set; }
        public int racunID { get; set; }
        public string OpisKvara { get; set; }
        public int PopravkaID { get; set; }
        public string Poslovnica { get; set; }
        public DateTime DatumPopravke { get; set; }
        
        public Popravke popravka { get; set; }
        public string Uposlenik { get;  set; }
        public string brojPopravke { get; set; }
        public int stavkaRacunaID { get; internal set; }
        public double cijenaPop { get; internal set; }

        public class Data
        {
            public int DioId { get; set; }
            public string UtroseniDio { get; set; }
            public int kolicina { get; set; }
            public double broj { get; set; }
            public double cijena { get; set; }
            public double Ukupno { get; set; }



        }
    }
}
