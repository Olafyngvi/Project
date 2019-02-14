using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutoServis.Models
{
    public class Grad : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Polje Naziv je obavezno !")]
        public string Naziv { get; set; }
    }
}
