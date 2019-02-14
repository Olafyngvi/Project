using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class InformacijeRacuna
    {
       
        public int racunID { get; set; }
        public string OpisKvara { get; set; }
        public int PopravkaID { get; set; }
        public string Poslovnica { get; set; }
        public DateTime DatumPopravke { get; set; }

        public Popravke popravka { get; set; }
        public string Uposlenik { get; set; }
        public double suma { get; set; }
        public double cijeaPop { get; set; }
        public string BrojPopravke { get; set; }
        public int Stavkaid { get; internal set; }
        public List<utrosak> ListaDijelova { get; internal set; }
        public string racunBroj { get; internal set; }
        public string UposlenikPrezime { get; internal set; }

        public class utrosak
        {

            public string UtroseniDio { get; set; }
            public int kolicina { get; set; }
         
            public double cijena { get; set; }
            public double sum { get; set; }



        }

        public double SumaUkupno(List<utrosak> list)
        {
            return 0;
        }
    }

}

