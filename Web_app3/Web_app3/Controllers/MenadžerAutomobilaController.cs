using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoServis.EF;
using AutoServis.Helper;
using AutoServis.Models;
using AutoServis.ViewModels;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;


using Microsoft.Extensions.Options;
using Nexmo.Api.Request;
using System.Dynamic;

namespace AutoServis.Controllers
{
    [Autorizacija(true, false, false, false)]
    public class MenadžerAutomobilaController : Controller
    {
        private MojContext _context;
        private readonly IHostingEnvironment he;
        private UploadSlika upload;


        public MenadžerAutomobilaController(IHostingEnvironment het, MojContext context)
        {
            _context = context;
            he = het;

        }





        public IActionResult ReportPregledi()
        {
            var model = _context.voziloprodaja.Where(x=>x.isDeleted==false).Include(v => v.Model).Include(v => v.Model.marka).OrderByDescending(x => x.brojPregleda);
            return PartialView(model);
        }

    
        public async Task<IActionResult> Index()
        {

            List<SortVM> lista = new List<SortVM>();
            
            lista.Insert(0, new SortVM { ID = 1,Naziv="Marka" });
            lista.Insert(1, new SortVM { ID = 2, Naziv = "Cijena" });
            


            ViewData["BrojVrataID"] = new SelectList(_context.brojvrata, "BrojVrataId", "Naziv");
            ViewData["GorivoID"] = new SelectList(_context.gorivo, "GorivoId", "Naziv");
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv");
            ViewData["OpremaID"] = new SelectList(_context.oprema, "OpremaId", "Naziv");
            ViewData["TipVozilaID"] = new SelectList(_context.tipvozila, "TipVozilaId", "Naziv");
            ViewData["TransmisijaID"] = new SelectList(_context.transmisija, "TransmisijaId", "Naziv");
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            ViewData["lista"] = new SelectList(lista,"ID","Naziv");


            var model = new VoziloProdajaVM
            {
                redovi = _context.voziloprodaja.Where(y => y.isDeleted == false)
                .Select(x => new VoziloProdajaVM.Auta
                {
                    VoziloProdajaID = x.VoziloProdajaID,
                    DatumProizvodnje = x.DatumProizvodnje,
                    Kilometraza = x.Kilometraza,
                    Kubikaza = x.Kubikaza,
                    SnagaMotora = x.SnagaMotora,
                    Cijena = x.Cijena,
                    Marka = x.Model.marka.Nazvi,
                    isDeleted = x.isDeleted,
                    brojVrata = x.BrojVrata.Naziv,
                    model = x.Model.Naziv,
                    tipVozila = x.TipVozila.Naziv,
                    oprema = x.Oprema.Naziv,
                    gorivo = x.Gorivo.Naziv,
                    transmisija = x.Transmisija.Naziv,
                    Sifra=x.SifraAutomobila,


                }).ToList()


            };


       
            return PartialView(model);
        }

        public IActionResult Sortiraj(int id)
        {
            List<SortVM> lista = new List<SortVM>();

            lista.Insert(0, new SortVM { ID = 1, Naziv = "Marka" });
            lista.Insert(1, new SortVM { ID = 2, Naziv = "Cijena" });



            ViewData["BrojVrataID"] = new SelectList(_context.brojvrata, "BrojVrataId", "Naziv");
            ViewData["GorivoID"] = new SelectList(_context.gorivo, "GorivoId", "Naziv");
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv");
            ViewData["OpremaID"] = new SelectList(_context.oprema, "OpremaId", "Naziv");
            ViewData["TipVozilaID"] = new SelectList(_context.tipvozila, "TipVozilaId", "Naziv");
            ViewData["TransmisijaID"] = new SelectList(_context.transmisija, "TransmisijaId", "Naziv");
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            ViewData["lista"] = new SelectList(lista, "ID", "Naziv");

            VoziloProdajaVM model = new VoziloProdajaVM();
            if (id == 1)
            {
                 model.vozilo = _context.voziloprodaja.Where(x=>x.isDeleted==false).Include(x => x.Model).Include(x => x.Model.marka).OrderBy(x => x.Model.marka.Nazvi).ToList();
                return PartialView(model);
            }
            else
            {
                model.vozilo = _context.voziloprodaja.Where(x => x.isDeleted == false).Include(x => x.Model).Include(x => x.Model.marka).Include(x => x.TipVozila).OrderBy(x => x.Cijena).ToList();
                return PartialView(model);
            }
        }


      
        public async Task<IActionResult> IndexProdanih()
        {


            ViewData["BrojVrataID"] = new SelectList(_context.brojvrata, "BrojVrataId", "Naziv");
            ViewData["GorivoID"] = new SelectList(_context.gorivo, "GorivoId", "Naziv");
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv");
            ViewData["OpremaID"] = new SelectList(_context.oprema, "OpremaId", "Naziv");
            ViewData["TipVozilaID"] = new SelectList(_context.tipvozila, "TipVozilaId", "Naziv");
            ViewData["TransmisijaID"] = new SelectList(_context.transmisija, "TransmisijaId", "Naziv");
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");

            var model = new VoziloProdajaVM
            {
                redovi = _context.voziloprodaja.Where(y => y.isDeleted == true)
                .Select(x => new VoziloProdajaVM.Auta
                {
                    VoziloProdajaID = x.VoziloProdajaID,
                    DatumProizvodnje = x.DatumProizvodnje,
                    Kilometraza = x.Kilometraza,
                    Kubikaza = x.Kubikaza,
                    SnagaMotora = x.SnagaMotora,
                    Cijena = x.Cijena,
                    Marka = x.Model.marka.Nazvi,
                    isDeleted = x.isDeleted,
                    brojVrata = x.BrojVrata.Naziv,
                    model = x.Model.Naziv,
                    tipVozila = x.TipVozila.Naziv,
                    oprema = x.Oprema.Naziv,
                    gorivo = x.Gorivo.Naziv,
                    transmisija = x.Transmisija.Naziv,
                    Sifra = x.SifraAutomobila,


                }).ToList()


            };


           
            return PartialView(model);
        }
     
        public async Task <IActionResult> Find (int? brojVrataID,int? ModelID, int? GorivoID, int? MarkaID, int? OpremaID, int? TipVozilaID, int? TransmisijaID, int? godina)
        {

            if (brojVrataID != null || GorivoID != null || ModelID != null || MarkaID != null || OpremaID != null || TipVozilaID != null || TransmisijaID != null || godina != null)
            {
                var vozila = from n in _context.voziloprodaja
                             select n;
              
                if (brojVrataID != null)
                {
                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.BrojVrata.BrojVrataId == brojVrataID);
                }
                if (ModelID != null)
                {
                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.Model.ModelId == ModelID);
                }
                if (GorivoID != null)
                {
                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.Gorivo.GorivoId == GorivoID);
                }
                if (MarkaID != null)
                {
                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.Model.marka.MarkaId == MarkaID);
                }
                if (TransmisijaID != null)
                {
                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.Transmisija.TransmisijaId == TransmisijaID);
                }
                if (TipVozilaID != null)
                {
                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.TipVozila.TipVozilaId == TipVozilaID);
                }
                if (OpremaID != null)
                {
                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.Oprema.OpremaId == OpremaID);
                }
                if (godina != null)
                {
                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.DatumProizvodnje.Year == godina);
                }

                var aVM = new VoziloProdajaVM();

                aVM.vozilo = await vozila.ToListAsync();




                return PartialView(aVM);
            }
            return new EmptyResult();
            }
        
        public List<Model> vratiModele(int ? MarkaID)
        {
            var modeli = _context.model.Where(x => x.MarkaID == MarkaID).ToList();

            return modeli;
            
        }
    




        public string Sifra(int broj)
        {

            string s = "AutoKucaNerVoziloBr" + ++broj;
            return s;
        }

    
        public async Task<IActionResult> Snimi(int ModelID, int TipVozilaID, int BrojVrataID, int OpremaID, int TransmisijaID, int GorivoID, DateTime DatumProizvodnje, string Kilometraza,IFormFile Slika, string Kubikaza, string SnagaMotora, double Cijena, DateTime datum, int poslovnica)
        {

            upload = new UploadSlika(he);
            VoziloProdaja vp = new VoziloProdaja
            {
                ModelID = ModelID,
                TipVozilaID = TipVozilaID,
                BrojVrataID = BrojVrataID,
                OpremaID = OpremaID,
                TransmisijaID = TransmisijaID,
                GorivoID = GorivoID,
                DatumProizvodnje = DatumProizvodnje,
                Kilometraza = Kilometraza,
                Kubikaza = Kubikaza,
                SnagaMotora = SnagaMotora,
                Cijena = Cijena,
                Slika=upload.Dodaj(Slika),
                SifraAutomobila = Sifra(_context.voziloprodaja.Count())

            };



           await _context.voziloprodaja.AddAsync(vp);

            VozilaPoslovnice vpp = new VozilaPoslovnice
            {
                DatumUvoza = datum,
                PoslovnicaID = poslovnica,
                VoziloProdajaID = vp.VoziloProdajaID
            };
            await _context.vozilaposlovnice.AddAsync(vpp);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Pocetna));
        }

        // GET: VoziloProdaja/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            DetaljiVozila detaljiVozila = new DetaljiVozila();
            if (id == null)
            {
                return NotFound();
            }

            detaljiVozila.vozilo = await _context.voziloprodaja
                .Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka)
                .SingleOrDefaultAsync(m => m.VoziloProdajaID == id);
           detaljiVozila.VozilaPoslovnice= _context.vozilaposlovnice.Where(x => x.VoziloProdajaID == detaljiVozila.vozilo.VoziloProdajaID).Include(x=>x.Poslovnica).SingleOrDefault();
            detaljiVozila.SlikeGalerije = _context.slike.Where(x => x.VoziloID == detaljiVozila.vozilo.VoziloProdajaID).ToList();
            detaljiVozila.ID = new List<int>();
            for (int i = 0; i < detaljiVozila.SlikeGalerije.Count; i++)
            {
                detaljiVozila.ID.Add(detaljiVozila.SlikeGalerije[i].Id);
            }
            return PartialView(detaljiVozila);
        }

       
        public IActionResult Create()
        {
            ViewData["BrojVrataID"] = new SelectList(_context.brojvrata, "BrojVrataId", "Naziv");
            ViewData["GorivoID"] = new SelectList(_context.gorivo, "GorivoId", "Naziv");
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv");
            ViewData["OpremaID"] = new SelectList(_context.oprema, "OpremaId", "Naziv");
            ViewData["TipVozilaID"] = new SelectList(_context.tipvozila, "TipVozilaId", "Naziv");
            ViewData["TransmisijaID"] = new SelectList(_context.transmisija, "TransmisijaId", "Naziv");
            ViewData["PoslovnicaID"] = new SelectList(_context.poslovnica, "Id", "Naziv");
            return View("Pocetna");
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voziloProdaja = await _context.voziloprodaja.Include(x => x.Model.marka).SingleOrDefaultAsync(m => m.VoziloProdajaID == id);
            if (voziloProdaja == null)
            {
                return NotFound();
            }
            ViewData["BrojVrataID"] = new SelectList(_context.brojvrata, "BrojVrataId", "Naziv", voziloProdaja.BrojVrata);
            ViewData["GorivoID"] = new SelectList(_context.gorivo, "GorivoId", "Naziv", voziloProdaja.Gorivo);
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv", voziloProdaja.Model);
            ViewData["OpremaID"] = new SelectList(_context.oprema, "OpremaId", "Naziv", voziloProdaja.Oprema);
            ViewData["TipVozilaID"] = new SelectList(_context.tipvozila, "TipVozilaId", "Naziv", voziloProdaja.TipVozila);
            ViewData["TransmisijaID"] = new SelectList(_context.transmisija, "TransmisijaId", "Naziv", voziloProdaja.Transmisija);
          
            return PartialView(voziloProdaja);
        }

     
        public IActionResult Update(int VoziloProdajaID, int TransmisijaID,
            DateTime DatumProizvodnje,
            string Kilometraza, int BrojVrataID, int TipVozilaID, string Kubikaza,
            string SnagaMotora, double Cijena, int OpremaID, bool isDeleted, int GorivoID,IFormFile Slika,string slikica, int ModelID)
        {
            upload = new UploadSlika(he);


            VoziloProdaja vp = _context.voziloprodaja.Find(VoziloProdajaID);

            if (Slika != null)
            {
                vp.TransmisijaID = TransmisijaID;

                vp.DatumProizvodnje = DatumProizvodnje;
                vp.Kilometraza = Kilometraza;
                vp.BrojVrataID = BrojVrataID;
                vp.TipVozilaID = TipVozilaID;
                vp.Kubikaza = Kubikaza;
                vp.SnagaMotora = SnagaMotora;
                vp.Cijena = Cijena;
                vp.Slika = null;
                vp.Slika = upload.Dodaj(Slika);
                vp.OpremaID = OpremaID;
                vp.isDeleted = isDeleted;
                vp.GorivoID = GorivoID;
                vp.ModelID = ModelID;
            }
            else
            {
                vp.TransmisijaID = TransmisijaID;

                vp.DatumProizvodnje = DatumProizvodnje;
                vp.Kilometraza = Kilometraza;
                vp.BrojVrataID = BrojVrataID;
                vp.TipVozilaID = TipVozilaID;
                vp.Kubikaza = Kubikaza;
                vp.SnagaMotora = SnagaMotora;
                vp.Cijena = Cijena;
                vp.Slika = slikica;
                vp.OpremaID = OpremaID;
                vp.isDeleted = isDeleted;
                vp.GorivoID = GorivoID;
                vp.ModelID = ModelID;
            }
           


            _context.SaveChanges();



            return RedirectToAction("Pocetna");
        }

        public IActionResult Obrisi(int id)
        {
            VoziloProdaja a = _context.voziloprodaja.Find(id);
            a.isDeleted = true;

         

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> AkcijaPregled(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voziloProdaja = await _context.voziloprodaja
                .Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka)
                .SingleOrDefaultAsync(m => m.VoziloProdajaID == id);
            if (voziloProdaja == null)
            {
                return NotFound();
            }

            return PartialView("Delete", voziloProdaja);
        }

        

        private bool VoziloProdajaExists(int id)
        {
            return _context.voziloprodaja.Any(e => e.VoziloProdajaID == id);
        }


        public async Task<IActionResult> IndexVrata()
        {
            return PartialView(await _context.brojvrata.ToListAsync());
        }


        public async Task<IActionResult> DetailsVrata(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brojVrata = await _context.brojvrata
                .SingleOrDefaultAsync(m => m.BrojVrataId == id);
            if (brojVrata == null)
            {
                return NotFound();
            }

            return View(brojVrata);
        }
        
        public IActionResult CreateVrata()
        {
            return View();
        }

        public async Task<IActionResult> SnimiVrata(string naziv)
        {
            var vrata = _context.brojvrata.Where(x => x.Naziv == naziv).SingleOrDefault();

            if (vrata == null)
            {
                BrojVrata brojVrata = new BrojVrata();
                brojVrata.Naziv = naziv;
                await _context.AddAsync(brojVrata);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });

            }



        }

 
        public async Task<IActionResult> EditVrata(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brojVrata = await _context.brojvrata.SingleOrDefaultAsync(m => m.BrojVrataId == id);
            if (brojVrata == null)
            {
                return NotFound();
            }
            return PartialView(brojVrata);
        }


 
        public  IActionResult UpdateVrata(int BrojVrataId,string Naziv)
        {
            BrojVrata brojVrata = _context.brojvrata.Find(BrojVrataId);
            brojVrata.Naziv = Naziv;
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }


      

        private bool BrojVrataExists(int id)
        {
            return _context.brojvrata.Any(e => e.BrojVrataId == id);
        }
        public async Task<IActionResult> IndexMarka()
        {
           
            return PartialView( await _context.marka.ToListAsync());
        }

   
        public async Task<IActionResult> DetailsMarka(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.marka
                .SingleOrDefaultAsync(m => m.MarkaId == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

     
        public IActionResult CreateMarka()
        {
            return View();
        }

   
       
        
        public IActionResult SnimiMarka(string marka)
        {
            var marka2 = _context.marka.Where(x => x.Nazvi == marka).SingleOrDefault();
            if (marka2 != null)
            {

                return Json(new { success = false, });

            }
            else
            {
                Marka marka1 = new Marka();
                marka1.Nazvi = marka;
                _context.Add(marka1);
                _context.SaveChanges();
                return Json(new { success = true });
            }






        }

   
        public async Task<IActionResult> EditMarka(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.marka.SingleOrDefaultAsync(m => m.MarkaId == id);
            if (marka == null)
            {
                return NotFound();
            }
            return PartialView(marka);
        }

   
        public  IActionResult UpdateMarka(int MarkaId,string Nazvi)
        {
            Marka marka = _context.marka.Find(MarkaId);
            marka.Nazvi = Nazvi;
            _context.SaveChanges();



            return RedirectToAction("Index");
        }

       

        private bool MarkaExists(int id)
        {
            return _context.marka.Any(e => e.MarkaId == id);
        }

        public async Task<IActionResult> IndexGorivo()
        {

            return PartialView(await _context.gorivo.ToListAsync());

        }




        public async Task<IActionResult> DetailsGorivo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gorivo = await _context.gorivo
                .SingleOrDefaultAsync(m => m.GorivoId == id);
            if (gorivo == null)
            {
                return NotFound();
            }

            return View(gorivo);
        }


        public IActionResult CreateGorivo()
        {
            return View();
        }



        public async Task<IActionResult> SnimiGorivo(string naziv)
        {

            var gorivo1 = _context.gorivo.Where(x => x.Naziv == naziv).SingleOrDefault();
            if (gorivo1 == null)
            {
                Gorivo gorivo = new Gorivo();
                gorivo.Naziv = naziv;
                await _context.AddAsync(gorivo);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });

            }

        }

   
        public async Task<IActionResult> EditGorivo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gorivo = await _context.gorivo.SingleOrDefaultAsync(m => m.GorivoId == id);
            if (gorivo == null)
            {
                return NotFound();
            }
            return PartialView(gorivo);
        }


        public  IActionResult UpdateGorivo(int GorivoId,string Naziv)
        {
            Gorivo gorivo = _context.gorivo.Find(GorivoId);
            gorivo.Naziv = Naziv;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

       
      

        private bool GorivoExists(int id)
        {
            return _context.gorivo.Any(e => e.GorivoId == id);
        }


        public async Task<IActionResult> IndexModel(string mod, int? markaID)
        {
            if (mod != null || markaID != null)
            {
                var m = from mo in _context.model
                        select mo;
                if (!String.IsNullOrEmpty(mod))
                {
                    m = m.Include(x => x.marka).Where(x => x.Naziv.Contains(mod));
                }

                if (markaID != null)
                {
                    m = m.Include(x => x.marka).Where(x => x.marka.MarkaId == markaID);
                }

                var model1 = new ModelVM();
                model1.model = await m.ToListAsync();
                return PartialView("FindModel", model1);
            }
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            var mojContext = _context.model.Include(m => m.marka);
            return PartialView(await mojContext.ToListAsync());
        }


        public async Task<IActionResult> DetailsModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model
                .Include(m => m.marka)
                .SingleOrDefaultAsync(m => m.ModelId == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

 
        public IActionResult CreateModel()
        {
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            return View();
        }


        public async Task<IActionResult> SnimiModel(string model1, int id)
        {
            var model2 = _context.model.Where(x => x.Naziv == model1).SingleOrDefault();

            if (model2==null)
            {
                Model model = new Model();
                model.Naziv = model1;
                model.MarkaID = id;
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return Json(new { success = true });

            }
            else
            {
                return Json(new { success = false, });

            }


            
        }

        public async Task<IActionResult> EditModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model.Include(x => x.marka).SingleOrDefaultAsync(m => m.ModelId == id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi", model.marka);
            return PartialView(model);
        }

        public  IActionResult UpdateModel(int ModelId, int MarkaID, string Naziv)
        {
            Model model = _context.model.Find(ModelId);
            model.Naziv = Naziv;
            model.MarkaID = MarkaID;
            _context.SaveChanges();
            

            
            return RedirectToAction("Index");
        }

       

        



        public async Task<IActionResult> EditConfrimedModel(int id, [Bind("ModelId,Naziv,MarkaID")] Model model)
        {
            if (id != model.ModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.ModelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi", model.marka);
            return View(model);
        }


        private bool ModelExists(int id)
        {
            return _context.model.Any(e => e.ModelId == id);
        }
       

     
        public async Task<IActionResult> IndexTip()
        {
            return PartialView(await _context.tipvozila.ToListAsync());
        }

        
        public async Task<IActionResult> DetailsTip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipVozila = await _context.tipvozila
                .SingleOrDefaultAsync(m => m.TipVozilaId == id);
            if (tipVozila == null)
            {
                return NotFound();
            }

            return View(tipVozila);
        }

    
        public IActionResult CreateTip()
        {
            return View();
        }


        public async Task<IActionResult> SnimiTip(string naziv)
        {
            var tip1 = _context.tipvozila.Where(x => x.Naziv == naziv).SingleOrDefault();

            if (tip1 == null)
            {
                TipVozila tipVozila = new TipVozila();
                tipVozila.Naziv = naziv;
                await _context.AddAsync(tipVozila);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });

            }




        }

        
        public IActionResult EditTip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipVozila = _context.tipvozila.Find(id);
            if (tipVozila == null)
            {
                return NotFound();
            }
            return PartialView(tipVozila);
        }


        public  IActionResult UpdateTip(int TipVozilaId,string Naziv)
        {
            TipVozila tip = _context.tipvozila.Find(TipVozilaId);
            tip.Naziv = Naziv;
            _context.SaveChanges();

            
            return RedirectToAction("Index");
        }

  
       
       

        private bool TipVozilaExists(int id)
        {
            return _context.tipvozila.Any(e => e.TipVozilaId == id);
        }

    
        public async Task<IActionResult> IndexTransmisija()
        {
            return PartialView(await _context.transmisija.ToListAsync());
        }

      
        public async Task<IActionResult> DetailsTransmisija(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transmisija = await _context.transmisija
                .SingleOrDefaultAsync(m => m.TransmisijaId == id);
            if (transmisija == null)
            {
                return NotFound();
            }

            return View(transmisija);
        }

      
        public IActionResult CreateTransmisija()
        {
            return View();
        }


     
        public async Task<IActionResult> SnimiTransmisija(string naziv)
        {
            var trans = _context.transmisija.Where(x => x.Naziv == naziv).SingleOrDefault();
            if (trans == null)
            {
                Transmisija transmisija = new Transmisija();
                transmisija.Naziv = naziv;
                await _context.AddAsync(transmisija);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });




        }

  
        public async Task<IActionResult> EditTransmisija(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transmisija = await _context.transmisija.SingleOrDefaultAsync(m => m.TransmisijaId == id);
            if (transmisija == null)
            {
                return NotFound();
            }
            return PartialView(transmisija);
        }

 
        public IActionResult UpdateTransmisija(int TransmisijaId,string Naziv)
        {
            Transmisija transmisija = _context.transmisija.Find(TransmisijaId);
            transmisija.Naziv = Naziv;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    


 

        private bool TransmisijaExists(int id)
        {
            return _context.transmisija.Any(e => e.TransmisijaId == id);
        }
      
        public async Task<IActionResult> IndexOprema()
        {
            return PartialView( await _context.oprema.ToListAsync());
        }

   
        public async Task<IActionResult> DetailsOprema(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.oprema
                .SingleOrDefaultAsync(m => m.OpremaId == id);
            if (oprema == null)
            {
                return NotFound();
            }

            return View(oprema);
        }


        public IActionResult CreateOprema()
        {
            return View();
        }

      
       
        public async Task<IActionResult> SnimiOprema(string naziv, string opis)
        {
            var oprema1 = _context.oprema.SingleOrDefault(x => x.Naziv == naziv);
            if (oprema1 == null)
            {
                Oprema oprema = new Oprema();
                oprema.Naziv = naziv;
                oprema.Opis = opis;
                await _context.AddAsync(oprema);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, });

            }



        }

      
        public async Task<IActionResult> EditOprema(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.oprema.SingleOrDefaultAsync(m => m.OpremaId == id);
            if (oprema == null)
            {
                return NotFound();
            }
            return PartialView(oprema);
        }


 
        public IActionResult UpdateOprema(int OpremaId, string Naziv, string Opis)
        {
            Oprema oprema = _context.oprema.Find(OpremaId);
            oprema.Naziv = Naziv;
            oprema.Opis = Opis;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    

        private bool OpremaExists(int id)
        {
            return _context.oprema.Any(e => e.OpremaId == id);
        }

   
        public async Task<IActionResult> SnimiS( int id, IFormFile slika)
        {
            Slike slike = new Slike();
            if (ModelState.IsValid)
            {
                if (slika != null)

                {
                    if (slika.Length > 0)

                   

                    {

                        byte[] p1 = null;
                        using (var fs1 = slika.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        slike.Slika = p1;

                    }
                }
                slike.VoziloID = id;
                _context.Add(slike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Pocetna));
            }
            ViewData["VoziloID"] = new SelectList(_context.voziloprodaja, "VoziloProdajaID", "VoziloProdajaID", slike.VoziloID);
            return View(slike);
        }

      




        
      
    


        

        
    
        bool nesto = true;
        public IActionResult DeleteMarka3(int? id)
        {
            var marka = _context.marka.SingleOrDefault(m => m.MarkaId == id);
            
            try
            {
                _context.marka.Remove(marka);

                _context.SaveChanges();
                nesto = true;
            }
            catch (DbUpdateException)
            {
                nesto = false;
               
                throw;
            }

            if (nesto==false)
            {
              
                return Json(new { success = false, });
            }
            else
            {
                
                return Json(new { success = true });
            }

        }

        public IActionResult ERROR()
        {
            return PartialView("error403");
        }

        public IActionResult DeleteS(int? id)
        {
            var slike = _context.slike.SingleOrDefault(m => m.Id == id);
            int ide = slike.VoziloID;

                _context.slike.Remove(slike);
                _context.SaveChanges();
            return RedirectToAction("Details/"+ide);
           
            

        }
        public IActionResult DeleteModel(int? id)
        {
            var model = _context.model.SingleOrDefault(m => m.ModelId == id);

            try
            {
                _context.model.Remove(model);

                _context.SaveChanges();
                nesto = true;
            }
            catch (DbUpdateException)
            {
                nesto = false;

                throw;
            }

            if (nesto == false)
            {
              
                return Json(new { success = false, });
            }
            else
            {
               
                return Json(new { success = true });
            }

        }



        public IActionResult DeleteTip(int? id)
        {
            var model = _context.tipvozila.SingleOrDefault(m => m.TipVozilaId == id);

            try
            {
                _context.tipvozila.Remove(model);

                _context.SaveChanges();
                nesto = true;
            }
            catch (DbUpdateException)
            {
                nesto = false;

                throw;
            }

            if (nesto == false)
            {
              
                return Json(new { success = false, });
            }
            else
            {
              
                return Json(new { success = true });
            }

        }


        public IActionResult DeleteGorivo(int? id)
        {
            var model = _context.gorivo.SingleOrDefault(m => m.GorivoId == id);

            try
            {
                _context.gorivo.Remove(model);

                _context.SaveChanges();
                nesto = true;
            }
            catch (DbUpdateException)
            {
                nesto = false;

                throw;
            }

            if (nesto == false)
            {
                
                return Json(new { success = false, });
            }
            else
            {
                
                return Json(new { success = true });
            }

        }



        public IActionResult DeleteTransmisija(int? id)
        {
            var model = _context.transmisija.SingleOrDefault(m => m.TransmisijaId == id);

            try
            {
                _context.transmisija.Remove(model);

                _context.SaveChanges();
                nesto = true;
            }
            catch (DbUpdateException)
            {
                nesto = false;

                throw;
            }

            if (nesto == false)
            {
               
                return Json(new { success = false, });
            }
            else
            {
               
                return Json(new { success = true });
            }

        }


        public IActionResult DeleteVrata(int? id)
        {
            var model = _context.brojvrata.SingleOrDefault(m => m.BrojVrataId == id);

            try
            {
                _context.brojvrata.Remove(model);

                _context.SaveChanges();
                nesto = true;
            }
            catch (DbUpdateException)
            {
                nesto = false;

                throw;
            }

            if (nesto == false)
            {
                
                return Json(new { success = false, });
            }
            else
            {
                
                return Json(new { success = true });
            }

        }


        public IActionResult DeleteOprema(int? id)
        {
            var model = _context.oprema.SingleOrDefault(m => m.OpremaId == id);

            try
            {
                _context.oprema.Remove(model);

                _context.SaveChanges();
                nesto = true;
            }
            catch (DbUpdateException)
            {
                nesto = false;

                throw;
            }

            if (nesto == false)
            {
             
                return Json(new { success = false, });
            }
            else
            {
              
                return Json(new { success = true });
            }

        }









        private bool SlikeExists(int id)
        {
            return _context.slike.Any(e => e.Id == id);
        }
        public IActionResult Pocetna()
        {
     

            ViewData["BrojVrataID"] = new SelectList(_context.brojvrata, "BrojVrataId", "Naziv");
            ViewData["GorivoID"] = new SelectList(_context.gorivo, "GorivoId", "Naziv");
            ViewData["ModelID"] = new SelectList(_context.model, "ModelId", "Naziv");
            ViewData["OpremaID"] = new SelectList(_context.oprema, "OpremaId", "Naziv");
            ViewData["TipVozilaID"] = new SelectList(_context.tipvozila, "TipVozilaId", "Naziv");
            ViewData["TransmisijaID"] = new SelectList(_context.transmisija, "TransmisijaId", "Naziv");
            ViewData["PoslovnicaID"] = new SelectList(_context.poslovnica.Where(x=>x.Zatvorena==false), "Id", "Naziv");
            ViewData["MarkaID"] = new SelectList(_context.marka, "MarkaId", "Nazvi");
            ViewData["VoziloProdajaID"] = new SelectList(_context.voziloprodaja, "VoziloProdajaID", "SifraAutomobila");
            ViewData["GradId"] = new SelectList(_context.grad, "Id", "Naziv");
            var model = new PocetnaVM();
             model.upitiVozila = _context.upitivozila.Where(x=>x.Pregledano==false).ToList();
            model.Slike = new Slike();
            model.broj = _context.upitivozila.Where(x => x.Pregledano == false).Count();
          
            return View("Početna", model);
        }

        public JsonResult VratiMarke()
        {
            var lista = _context.marka.ToList();
            lista.Insert(0, new Marka { MarkaId = 0, Nazvi = "Odaberite marku vozila" });

            return Json(new SelectList(lista, "MarkaId", "Nazvi"));
        }

        public JsonResult VratiMarke2()
        {
            var lista = (from mod in _context.model.Include(x => x.marka)
                          select new
                          {
                              ID = mod.ModelId,
                              Naziv= mod.Naziv + " | " + mod.marka.Nazvi

                          }).ToList();

          

            return Json(new SelectList(lista, "ID", "Naziv"));
        }
        public JsonResult VratiOpremu()
        {
            var lista = _context.oprema.ToList();
          

            return Json(new SelectList(lista, "OpremaId", "Naziv"));
        }
    }
}