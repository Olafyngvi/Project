using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class KontaktUpiti
    {
        public int Id { get; set; }
        public string ImePrezime { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Poruka { get; set; }
        public bool Pregledano { get; set; }
    }
}
