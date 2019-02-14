using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int DioId { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }
        public Dio Dio { get; set; }
        public Order Order { get; set; }
    }
}
