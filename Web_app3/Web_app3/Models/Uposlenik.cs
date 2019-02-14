using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Uposlenik
    {
        public int Id { get; set; }
        public DateTime DatumZaposljavanja { get; set; }
        public  string JMBG { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public int VrstaUposlenikaId { get; set; }
        public VrstaUposlenika VrstaUposlenika { get; set; }
        public int PoslovnicaId { get; set; }
        public Poslovnica Poslovnica { get; set; }
        public virtual Osoba Osoba { get; set; }
        public int? OsobaId { get; set; }
        public bool Neaktivan { get; set; }
    }
}
