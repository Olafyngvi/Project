using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class VoziloPopravka
    {
        public int VoziloPopravkaID { get; set; }
        public int GodinaProizvodnje { get; set; }
        public Gorivo Gorivo { get; set; }
        public int GorivoID { get; set; }
        public Popravke Popravke { get; set; }
        public int PopravkeID { get; set; }
        public Model Model { get; set; }
        public int ModelID { get; set; }
    }
}
