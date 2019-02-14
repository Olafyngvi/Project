using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class Paging
    {
        public int UkupnoItema { get; set; }
        public int ItemaPoStranici { get; set; }
        public int TrenutnaStranica { get; set; }
        public int TotalPages =>
        (int)Math.Ceiling((decimal)UkupnoItema / ItemaPoStranici);
    }
}
