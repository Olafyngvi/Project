using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class NadjenaPopravkaVM
    {

            public int popravkaId { get; set; }
            public string brojPopravke { get; internal set; }
            public DateTime datumPopravke { get; internal set; }
        
    }
}
