using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Marka
    {
        public int MarkaId { get; set; }
        [Required]
        public string Nazvi { get; set; }
    }
}
