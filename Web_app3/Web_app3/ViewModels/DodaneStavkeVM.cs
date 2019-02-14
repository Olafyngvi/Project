using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class DodaneStavkeVM
    {

        public List<Data> podaci { get; set; }

        public class Data
        {
            public int ID { get; internal set; }
      
            public int dio { get; internal set; }
            public int kolicina { get; internal set; }
            public double cijena { get; internal set; }
        }
    }
}
