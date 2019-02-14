using AutoServis.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";
        public static void SetLogiraniKorisnik(this HttpContext context, Uposlenik korisnik,bool snimiUCookie = false)
        {
            context.Session.Set(LogiraniKorisnik, korisnik);
        }
        public static Uposlenik GetLogiraniKorisnik(this HttpContext context)
        {
            Uposlenik korisnik = context.Session.Get<Uposlenik>(LogiraniKorisnik);
            return korisnik;
        }
    }
}
