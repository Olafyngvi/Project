using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServis.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoServis.ViewModels
{
    public class PopravkaStavkeVM
    {
        public int IDPopravke { get; set; }
        public string BrojPopravke { get; set; }
      
       
        public string datum { get; set; }
        public string poslovnica { get; set; }
        public int RID { get; set; }
        public double cijena { get; set; }

       
        
    }
}
