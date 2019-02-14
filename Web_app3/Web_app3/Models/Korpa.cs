using AutoServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Models
{
    public class Korpa
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Dio dio, int quantity)
        {
            CartLine line = lineCollection
            .Where(p => p._dio.DioId == dio.DioId)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    _dio = dio,
                    Quantity = quantity
                });
            }
            else {
                line.Quantity += quantity;
                }
        }
        public virtual void RemoveLine(Dio dio) =>
        lineCollection.RemoveAll(l => l._dio.DioId == dio.DioId);
        public virtual double ComputeTotalValue() =>
        lineCollection.Sum(e => e._dio.Cijena * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
}
public class CartLine
{
    public int CartLineID { get; set; }
    public Dio _dio { get; set; }
    public int Quantity { get; set; }
}
}
