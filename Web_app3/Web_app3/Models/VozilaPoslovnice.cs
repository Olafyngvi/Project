using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.Models;

namespace AutoServis.Models
{
    public class VozilaPoslovnice
    {
        public int VozilaPoslovniceId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumUvoza { get; set; }
        public Poslovnica Poslovnica { get; set; }
        public int PoslovnicaID { get; set; }
        public VoziloProdaja VoziloProdaja { get; set; }
        public int VoziloProdajaID { get; set; }

    }
}
