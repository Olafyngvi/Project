using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class ModelMarka
    {
        public List<nesto> m { get; set; }
        public int ID { get; set; }
        public string model { get; set; }
        public string marka { get; set; }

        public class nesto
        {
            public int ID { get; set; }
            public string model { get; set; }
            public string marka { get; set; }
        }
    }
}
