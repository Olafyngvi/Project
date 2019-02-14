using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.Models;

namespace AutoServis.Models
{
    public class StavkeRacunaOnline
    {
        public int StavkeRacunaOnlineId { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }
        public Dio Dio { get; set; }
        public int DioID { get; set; }
        public Racun Racun { get; set; }
        public int RacunID { get; set; }
        public Klijent Klijent { get; set; }
        public int KlijentID { get; set; }
    }
}
