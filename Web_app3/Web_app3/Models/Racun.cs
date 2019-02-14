using System;


namespace AutoServis.Models
{
    public class Racun
    {
        public int RacunId { get; set; }
        public string BrojRacuna { get; set; }
        public DateTime Datum { get; set; }
        public double Ukupno { get; set; }
        public bool Online { get; set; }
        public NacinPlacanja nacinPlacanja { get; set; }
        public int NacinPlacanjaID { get; set; }
    }
}
