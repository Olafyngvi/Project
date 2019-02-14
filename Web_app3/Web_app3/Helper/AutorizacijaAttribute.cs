using AutoServis.EF;
using AutoServis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool UposlenikAutomobili, bool UposlenikPopravke, bool UposlenikDijelovi, bool Administrator) 
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { UposlenikAutomobili , UposlenikPopravke , UposlenikDijelovi , Administrator };
        }

       
    }
    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool UposlenikAutomobili, bool UposlenikPopravke, bool UposlenikDijelovi, bool Administrator)
        {
            _UposlenikAutomobili = UposlenikAutomobili;
            _UposlenikPopravke = UposlenikPopravke;
            _UposlenikDijelovi = UposlenikDijelovi;
            _Administrator = Administrator;
        }
        private readonly bool _UposlenikAutomobili;
        private readonly bool _UposlenikPopravke;
        private readonly bool _UposlenikDijelovi;
        private readonly bool _Administrator;

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext,ActionExecutionDelegate next)
        {

            Uposlenik k = filterContext.HttpContext.GetLogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani!";

                }
                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { area = "" });
                return;
            }

            MojContext db = filterContext.HttpContext.RequestServices.GetService<MojContext>();

            if (_UposlenikAutomobili && db.uposlenik.Where(x=>x.VrstaUposlenikaId==2).Any(x=>x.Id==k.Id))
            {
                await next();
                return;
            }

            if (_UposlenikDijelovi && db.uposlenik.Where(x => x.VrstaUposlenikaId == 3).Any(x => x.Id == k.Id))
            {
                await next();
                return;

            }

            if (_UposlenikPopravke && db.uposlenik.Where(x => x.VrstaUposlenikaId == 4).Any(x => x.Id == k.Id))
            {
                await next();
                return;

            }

            if (_Administrator && db.uposlenik.Where(x => x.VrstaUposlenikaId == 1).Any(x => x.Id == k.Id))
            {
                await next();
                return;

            }

            if (filterContext.Controller is Controller controllerr)
            {
                controllerr.TempData["error_poruka"] = "Nemate pravo pristupa!";

            }
            

            filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { area = "" });
            return;


        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }


    }
}
