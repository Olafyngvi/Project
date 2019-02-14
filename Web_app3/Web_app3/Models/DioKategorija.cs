using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class DioKategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public byte[] Slika { get; set; }
    }
}
