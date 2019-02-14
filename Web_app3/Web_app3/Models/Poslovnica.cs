using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Poslovnica : IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }
        public bool Zatvorena { get; set; }
       
     
    }
}
