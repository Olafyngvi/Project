using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class VrstaUposlenika : IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
