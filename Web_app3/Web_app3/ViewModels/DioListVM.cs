using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class DioListVM
    {
        public IEnumerable<Dio> Dijelovi { get; set; }
        public Paging Paging { get; set; }
        public string TrenutnaMarka { get; set; }
        public string TrenutniModel { get; set; }
        public IEnumerable<DioKategorija> kategorije{ get; set; }
    }
}
