using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoServis.EF;
using AutoServis.Models;
using AutoServis.ViewModels;
using AutoServis.Helper;
using Microsoft.AspNetCore.Http;


namespace AutoServis.Controllers
{

    [Autorizacija(false, true, false, false)]
    public class PopravkaController : Controller
    {
        private readonly MojContext _context;
        

        public PopravkaController(MojContext context)
        {
            _context = context;
        }

        

        public async Task<IActionResult> Index()
        {
            var mojContext = _context.popravka.Include(p => p.Poslovnica).Include(p => p.Uposlenik);
            return View(await mojContext.ToListAsync());
        }


        
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var popravke = await _context.popravka
                .Include(p => p.Poslovnica)
                .Include(p => p.Uposlenik)
                .Include(p => p.Uposlenik.Osoba)
                .SingleOrDefaultAsync(m => m.PopravkeId == id);
            if (popravke == null)
            {
                return NotFound();
            }
            List<string> stavke = _context.stavkeracuna.Include(d => d.Dio).Where(x => x.PopravkeID == popravke.PopravkeId).Select(w => w.Dio.Naziv).ToList();

            ViewBag.listaStavki = stavke;

            return View(popravke);
        }
        

        public ActionResult DodajStavku(int popID, int dio,int Rid,string PopravkaBr,double cijenaPopravke,DateTime DatumPopa,int kolicina=0)
        {
 
            StavkeRacuna NovaStavka = new StavkeRacuna
            {

                Dio = _context.dio.Where(x => x.DioId == dio).Select(d => d).SingleOrDefault(),
                Cijena = _context.dio.Where(z=>z.DioId==dio).Select(n=>n.Cijena).SingleOrDefault(),
                Kolicina = kolicina,
                Racun = _context.racun.Where(x=>x.RacunId==Rid).Select(d => d).FirstOrDefault(),
                Popravke = _context.popravka.Where(g => g.PopravkeId == popID).Select(d => d).FirstOrDefault(),


            };
            _context.stavkeracuna.Add(NovaStavka);
            _context.SaveChanges();
            PripremaRacunaVM model = new PripremaRacunaVM
            {
                lista = _context.stavkeracuna
              .Where(g => g.PopravkeID == popID)
              .Include(d => d.Dio)
              .Select(x => new PripremaRacunaVM.Data
              {
                  DioId =x.DioID,
                  UtroseniDio = _context.dio.Where(g => g.DioId == x.DioID).Select(v => v.Naziv).SingleOrDefault(),

                  kolicina = x.Kolicina,

                  cijena = _context.dio.Where(g => g.DioId == x.DioID).Select(v => v.Cijena).SingleOrDefault(),




              }).ToList(),
                racunID = _context.racun.Where(x => x.RacunId == Rid).Select(d => d.RacunId).FirstOrDefault(),
                OpisKvara = _context.popravka.Where(g => g.PopravkeId == popID).Select(d => d.OpisKvara).FirstOrDefault(),
                PopravkaID = _context.popravka.Where(g => g.PopravkeId == popID).Select(d => d.PopravkeId).FirstOrDefault(),
                Poslovnica = _context.popravka.Where(g => g.PopravkeId == popID).Select(d => d.Poslovnica.Naziv).FirstOrDefault(),
                DatumPopravke = DatumPopa,
                Uposlenik = _context.popravka.Where(g => g.PopravkeId == popID).Select(d => d.Uposlenik.Osoba.Ime).FirstOrDefault(),
                brojPopravke = PopravkaBr,
                stavkaRacunaID = NovaStavka.StavkeRacunaId,
                cijenaPop = cijenaPopravke
            };

           


            return PartialView("DodaneStavke", model);
          
        }
       
        public string GenerisiBrojPopravke(int id, bool a)
        {
            string brojPopravke;
            if (a==true)
            {
                 brojPopravke = "PB " + id + "/" + id * 100;
            }
            else
                 brojPopravke = "RB " + id + "/" + id * 100;
            return brojPopravke;
        }

     


        // GET: Popravka/Create
        public IActionResult Popravka()
        {
            var uposlenik = HttpContext.GetLogiraniKorisnik();
            

            string ime = _context.osoba.Where(x => x.Id == uposlenik.OsobaId).Include(a => a.Uposlenik).Select(v => v.Ime).FirstOrDefault();
            string prezime = _context.osoba.Where(x => x.Id == uposlenik.OsobaId).Include(a => a.Uposlenik).Select(v =>v.Prezime).FirstOrDefault();
            string imePrezime = ime + " " + prezime;
            string poslovnica = _context.poslovnica.Where(x => x.Id == uposlenik.PoslovnicaId).Select(v => v.Naziv).FirstOrDefault();

            ViewData["ime"] = imePrezime;
            ViewData["poslovnica"] = poslovnica;
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            ViewData["Gorivo"] = new SelectList(_context.gorivo, "GorivoId", "Naziv");
            return View("Create");
        }



        public IActionResult Pretraga(string brojPop)
        {
            string br = brojPop.Substring(2, 4);

            NadjenaPopravkaVM model = new NadjenaPopravkaVM
            {
                    popravkaId= _context.popravka.Where(x => x.BrojPopravke == brojPop).Select(v => v.PopravkeId).FirstOrDefault(),
                brojPopravke = _context.popravka.Where(x => x.BrojPopravke == brojPop).Select(v =>v.BrojPopravke).FirstOrDefault(),
                    datumPopravke = _context.popravka.Where(x => x.BrojPopravke == brojPop).Select(v => v.DatumPopravke).FirstOrDefault()
            };
            return PartialView("_pretragaPopravke",model);
        }





        public IActionResult DodajPopravku(string OpisKvara,DateTime Datum, double CijenaPopravke,int MarkaID,int ModelID,int godinaPr,int Gorivo)
        {
            var uposlenik = HttpContext.GetLogiraniKorisnik();
            Popravke pr = new Popravke
            {
                OpisKvara = OpisKvara,
                DatumPopravke = Datum,
                BrojPopravke ="NA",
                CijenaPopravke = CijenaPopravke,
                Poslovnica = _context.poslovnica.Where(x => x.Id == uposlenik.PoslovnicaId).FirstOrDefault(),
                Uposlenik = _context.uposlenik.Where(x => x.Id == uposlenik.Id).Select(u => u).FirstOrDefault()
            };

            _context.popravka.Add(pr);
            _context.SaveChanges();

            pr.BrojPopravke = GenerisiBrojPopravke(pr.PopravkeId, true);
            _context.popravka.Update(pr);
            _context.SaveChanges();

            Racun racun = new Racun
            {
                Datum = pr.DatumPopravke,
                NacinPlacanjaID=1,
            };
            _context.racun.Add(racun);
            _context.SaveChanges();

            VoziloPopravka vp = new VoziloPopravka
            {
                Model = _context.model.Where(x => x.ModelId == ModelID).FirstOrDefault(),
                GodinaProizvodnje = godinaPr,
                Gorivo = _context.gorivo.Where(x => x.GorivoId == Gorivo).FirstOrDefault(),
                Popravke = _context.popravka.Where(x => x.PopravkeId == pr.PopravkeId).FirstOrDefault()

            };

            _context.vozilopopravka.Add(vp);

            _context.SaveChanges();



            Popravke p = pr;


            PopravkaStavkeVM model = new PopravkaStavkeVM
            {
                IDPopravke = p.PopravkeId,
                BrojPopravke =p.BrojPopravke,

                datum = String.Format("{0:dd/MM/yyyy}", p.DatumPopravke),
                cijena = CijenaPopravke,
                poslovnica = _context.poslovnica.Where(x => x.Id == p.PoslovnicaID).Select(v => v.Naziv).SingleOrDefault(),
                RID = racun.RacunId,
             
   
            };

            ViewData["dijelovi"] =  new SelectList(_context.dio.Where(x=>x.ModelID==vp.ModelID), "DioId", "Naziv");

            return View("StavkePopravke", model);
        }

        public JsonResult VratiModele(int? id)
        {
            List<Model> lista = new List<Model>();

            lista = _context.model.Where(x => x.MarkaID == id).ToList();
            var vozila = _context.voziloprodaja.ToList();
            List<Model> lista2 = new List<Model>();



            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = 0; j < vozila.Count; j++)
                {
                    if (vozila[j].ModelID == lista[i].ModelId)
                    {
                        var ide = lista[i].ModelId;
                        Model nepostiji = null;
                        nepostiji = lista2.Find(x => x.ModelId == ide);
                        if (nepostiji == null)
                        {
                            lista2.Add(lista[i]);

                        }
                    }
                }
            }
            var broj = lista2.Count();
            lista2.Insert(0, new Model { ModelId = 0, Naziv = "Svi modeli" });

            return Json(new SelectList(lista2, "ModelId", "Naziv"));


        }



        public IActionResult Racun(double sum, int rajdi, string OpisK, int popravkaID,double cijenaRuku, string poslovnica, DateTime datum, string Uposlenik, string BrPopravke, int stavkaID)
        {
            InformacijeRacuna model = new InformacijeRacuna
            {
                OpisKvara = OpisK,
                racunID = rajdi,
                suma = sum,
                racunBroj = GenerisiBrojPopravke(rajdi,false),
                DatumPopravke = datum,
                PopravkaID = popravkaID,
                Poslovnica = poslovnica,
                Uposlenik = Uposlenik,
                BrojPopravke = BrPopravke,
                Stavkaid = stavkaID,
                ListaDijelova = _context.stavkeracuna
                .Where(x => x.PopravkeID == popravkaID)
                .Include(d => d.Dio)
                .Select(v => new InformacijeRacuna.utrosak
                {
                    UtroseniDio = v.Dio.Naziv,
                    kolicina = v.Kolicina,
                    cijena = v.Cijena
                }).ToList(),
                cijeaPop = cijenaRuku
            };
        

            return View("PrintRacuna",model);
        }
     
        
      

        

       
    }
}
