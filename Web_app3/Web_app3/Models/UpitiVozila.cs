using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class UpitiVozila
    {
        public int ID { get; set; }
        public string ImeiPrezime { get; set; }
        public string Email { get; set; }
        public string SifraAutomobila { get; set; }
        public DateTime DatumVrijem { get; set; }
        public bool Pregledano { get; set; }
        public string Text { get; set; }
    }
}
