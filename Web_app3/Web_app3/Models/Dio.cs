using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Dio
    {
        public int DioId { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public string Sifra { get; set; }
        public DioKategorija Kategorija { get; set; }
        public int? KategorijaId { get; set; }
        public bool IsDeleted { get; set; }
        public Model model { get; set; }
        public int ModelID { get; set; }
    }
}
