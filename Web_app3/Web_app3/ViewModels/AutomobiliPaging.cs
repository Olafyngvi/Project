using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AutoServis.ViewModels
{
    public class AutomobiliPaging<T>:List<T>
    {
        public int TrenutnaStranica { get; private set; }
        public int UkupnoStranica { get; private set; }
        public int brojVozilaNastranici { get; private set; }
        public int UkupnoVozila { get; private set; }
        public int brojac { get; set; }

 

        public bool HasPreviousPage
        {
            get
            {
                return (TrenutnaStranica > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (TrenutnaStranica < UkupnoStranica);
            }
        }
        public AutomobiliPaging(List<T> model, int ukupnoVoz, int brojStranice, int ukupnoVozpoStranici)
        {
            UkupnoVozila = ukupnoVoz;
            brojVozilaNastranici = brojVozilaNastranici;
            TrenutnaStranica = brojStranice;
            UkupnoStranica = (int)Math.Ceiling(ukupnoVoz / (double)ukupnoVozpoStranici);
            
            AddRange(model);
        }

        public static AutomobiliPaging<T> Create(IQueryable<T> izvor, int brojStranice, int brojPostranici)
        {
            var totalCount = izvor.Count();
            var items = izvor.Skip((brojStranice - 1) * brojPostranici).Take(brojPostranici).ToList();
            return new AutomobiliPaging<T>(items, totalCount, brojStranice, brojPostranici);
        }
    } 

}
 
