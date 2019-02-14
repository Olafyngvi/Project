using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Slike
    {
        public int Id { get; set; }
        public VoziloProdaja Vozilo { get; set; }
        public int VoziloID { get; set; }
        public string slikaUrl { get; set; }
        public byte[] Slika { get; set; }
       
    }
}
