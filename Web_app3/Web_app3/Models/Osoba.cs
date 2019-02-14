using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AutoServis.Models
{
    public class Osoba
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ime je obavezno polje")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Adresa je obavezno polje")]
        public string Adresa { get; set; }
        public string KorisnickoIme { get; set; }
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
        public string Telefon { get; set; }
        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }
        public Klijent Klijent { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember]
        public Uposlenik Uposlenik { get; set; }
    }
}
