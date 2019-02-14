using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class DetaljiVozila
    {

        public VoziloProdaja  vozilo { get; set; }
        public VozilaPoslovnice VozilaPoslovnice { get; set; }
        public List<Slike> SlikeGalerije { get; set; }
        public List<int> ID { get; set; }

    }
}
