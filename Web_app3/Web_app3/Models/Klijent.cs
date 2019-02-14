using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Klijent
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumRegistracije { get; set; }
        public int BrojNarudzbi { get; set; }
        public virtual Osoba Osoba { get; set; }
        public int OsobaId { get; set; }

       
    }
}
