using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Model
    {
        public int ModelId { get; set; }
        [Required]
        public string Naziv { get; set; }
        public Marka marka {get;set;}
        public int MarkaID { get; set; }

    }
}
