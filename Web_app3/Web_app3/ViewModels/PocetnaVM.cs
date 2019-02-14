using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class PocetnaVM
    {
        public List<UpitiVozila> upitiVozila { get; set; }
        public Slike Slike { get; set; }
        public int broj { get; set; }
    }
}
