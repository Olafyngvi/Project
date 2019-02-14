using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class DetaljiVM
    {
        public VoziloProdaja Auta { get; set; }
        public List<Slike> Slike { get; set; }
    }
}
