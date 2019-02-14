using System.Linq;
using System.Threading.Tasks;
using AutoServis.EF;
using AutoServis.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoServis.Models;
using ReflectionIT.Mvc.Paging;
using System.Collections.Generic;

namespace AutoServis.Controllers
{

    public class AutomobiliKlijentController : Controller
    {

        private MojContext _context;

        public AutomobiliKlijentController(MojContext context)
        {
            _context = context;
        }

    
        
        public async Task<IActionResult> Index(int ? ModelID, 
         int? MarkaID, int? TipVoziilaID, int? TransmisijaID, int? godina, int? page)
        {
            if ( MarkaID != null ||  TipVoziilaID != null || TransmisijaID != null || godina != null)
            {

                var vozila = from n in _context.voziloprodaja
                             where n.isDeleted==false
                             select n;
                
               
               
             
                if (MarkaID != null)
                {

                    page = 1;

                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.Model.marka.MarkaId == MarkaID);
                }
                if (ModelID >0)
                {
                    page = 1;


                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.Model.ModelId == ModelID);
                }
                if (TransmisijaID != null)
                {
                    page = 1;


                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.Transmisija.TransmisijaId == TransmisijaID);
                }

                if (TipVoziilaID != null)
                {
                    page = 1;


                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.TipVozila.TipVozilaId == TipVoziilaID);
                }

                if (godina != null)
                {
                    page = 1;

                    vozila = vozila.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).Where(v => v.DatumProizvodnje.Year == godina);
                }

                ViewData["BrojVrataID"] = new SelectList(_context.brojvrata, "BrojVrataId", "Naziv");
                ViewData["GorivoID"] = new SelectList(_context.gorivo, "GorivoId", "Naziv");
                ViewData["OpremaID"] = new SelectList(_context.oprema, "OpremaId", "Naziv");
                ViewData["TipVozilaID"] = new SelectList(_context.tipvozila, "TipVozilaId", "Naziv");
                ViewData["TransmisijaID"] = new SelectList(_context.transmisija, "TransmisijaId", "Naziv");

                List<Marka> listaMarke1 = new List<Marka>();
                listaMarke1 = _context.marka.ToList();
                List<VoziloProdaja> listaVozila1 = new List<VoziloProdaja>();
                listaVozila1 = _context.voziloprodaja.Include(x => x.Model).Include(x => x.Model.marka).ToList();
                List<Marka> listaKonačna1 = new List<Marka>();
                for (int i = 0; i < listaMarke1.Count; i++)
                {
                    for (int j = 0; j < listaVozila1.Count; j++)
                    {
                        if (listaVozila1[j].Model.MarkaID == listaMarke1[i].MarkaId)
                        {

                            Marka nepostoji = null;
                            nepostoji = listaKonačna1.Find(x => x.MarkaId == listaMarke1[i].MarkaId);
                            if (nepostoji == null)
                            {
                                listaKonačna1.Add(listaMarke1[i]);

                            }

                        }
                    }
                }
                ViewData["MarkaID"] = new SelectList(listaKonačna1, "MarkaId", "Nazvi");



                var auta = vozila;
                ViewData["brojAuta"] = auta.Count();
                return View("Find",auta);
            }
            ViewData["BrojVrataID"] = new SelectList(_context.brojvrata, "BrojVrataId", "Naziv");
            ViewData["GorivoID"] = new SelectList(_context.gorivo, "GorivoId", "Naziv");
            ViewData["OpremaID"] = new SelectList(_context.oprema, "OpremaId", "Naziv");
            ViewData["TipVozilaID"] = new SelectList(_context.tipvozila, "TipVozilaId", "Naziv");
            ViewData["TransmisijaID"] = new SelectList(_context.transmisija, "TransmisijaId", "Naziv");

            List<Marka> listaMarke = new List<Marka>();
            listaMarke = _context.marka.ToList();
            List<VoziloProdaja> listaVozila = new List<VoziloProdaja>();
            listaVozila = _context.voziloprodaja.Include(x=>x.Model).Include(x=>x.Model.marka).ToList();
            List<Marka> listaKonačna = new List<Marka>();
            for (int i = 0; i < listaMarke.Count; i++)
            {
                for (int j = 0; j < listaVozila.Count; j++)
                {
                    if (listaVozila[j].Model.MarkaID == listaMarke[i].MarkaId)
                    {

                        Marka nepostoji = null;
                        nepostoji = listaKonačna.Find(x => x.MarkaId == listaMarke[i].MarkaId);
                        if (nepostoji == null)
                        {
                            listaKonačna.Add(listaMarke[i]);

                        }

                    }
                }
            }
            
            ViewData["MarkaID"] = new SelectList(listaKonačna, "MarkaId", "Nazvi");


            int pageSize = 6;
            var item = _context.voziloprodaja.Include(x => x.Model).Include(x => x.Model.marka).Where(x=>x.isDeleted==false).AsNoTracking();
            var model =  AutomobiliPaging<VoziloProdaja>.Create(item, page ??1,pageSize);
            return  View("Index2",model);
            
        }



        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VoziloProdaja voziloProdaja = _context.voziloprodaja.Find(id);
            voziloProdaja.brojPregleda++;
            _context.SaveChanges();
            var slike = _context.slike.Where(s => s.VoziloID == id).ToList();
            var autoVM = new DetaljiVM();
            autoVM.Auta= _context.voziloprodaja.Include(v => v.BrojVrata)
                .Include(v => v.Gorivo)
                .Include(v => v.Model)
                .Include(v => v.Oprema)
                .Include(v => v.TipVozila)
                .Include(v => v.Transmisija)
                .Include(v => v.Model.marka).SingleOrDefault(v => v.VoziloProdajaID == id);
            autoVM.Slike = slike;
            return View(autoVM);

        }

        public JsonResult VratiModele(int? id)
        {
            List<Model> lista = new List<Model>();
           
            lista = _context.model.Where(x => x.MarkaID == id).ToList();
            var vozila = _context.voziloprodaja.ToList();
            List<Model> lista2 = new List<Model>();

         

            for (int i = 0; i < lista.Count; i++)
            {
                for (int j  = 0; j < vozila.Count; j++)
                {
                    if (vozila[j].ModelID == lista[i].ModelId) {
                        var ide = lista[i].ModelId;
                        Model nepostiji = null;
                         nepostiji = lista2.Find(x=>x.ModelId==ide);
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


     




    }
}