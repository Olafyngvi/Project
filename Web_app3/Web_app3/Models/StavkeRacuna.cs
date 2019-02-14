
namespace AutoServis.Models
{
    public class StavkeRacuna
    {
        public int StavkeRacunaId { get; set; }
        public Racun Racun { get; set; }
        public int RacunID { get; set; }
        public Dio Dio { get; set; }
        public int DioID { get; set; }
        public Popravke Popravke { get; set; }
        public int PopravkeID { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }
    }
}
