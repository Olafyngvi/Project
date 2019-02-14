using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        public List<OrderDetails> OrderLines { get; set; }
        [Required(ErrorMessage = "Molimo unesite Vase ime")]
        [StringLength(50)]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Molimo unesite Vase prezime")]
        [StringLength(50)]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Molimo unesite Vasu adresu")]
        [StringLength(100)]
        public string Adresa { get; set; }
        [Required(ErrorMessage ="Molimo unesite naziv grada")]
        [StringLength(20)]
        public string Grad { get; set; }
        [Required(ErrorMessage = "Molimo unesite postanski broj grada")]
        [Display(Name = "Postanski broj")]
        [StringLength(10, MinimumLength = 4)]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Molimo unesite naziv drzave")]
        [StringLength(50)]
        public string Drzava { get; set; }
        [Required(ErrorMessage = "Molimo unesite Vas broj telefona")]
        [StringLength(25)]
        public string BrojTelefona { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "Email adresa nije u ispravnom formatu")]
        public string Email { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public double Ukupno { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime DatumNarudzbe { get; set; }
        public bool Zavrsena { get; set; }
    }
}
