using AutoServis.EF;
using AutoServis.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AutoServis.Helper
{
    public class UploadSlika
    {
      
       
        private readonly IHostingEnvironment he;

        public UploadSlika(IHostingEnvironment het)
        {
            he = het;
          
            
        }

        public string  Dodaj(IFormFile slika)
        {



            if (slika != null)
            {

        var nazivSlike = ContentDispositionHeaderValue.Parse(slika.ContentDisposition).FileName.Trim('"');
               
                var folder = Path.Combine(he.WebRootPath, string.Format("lib\\SlikeVozila\\"));



                var savePath = Path.Combine(folder, nazivSlike);
                slika.CopyTo(new FileStream(savePath, FileMode.Create));

                string getPath = "/lib/SlikeVozila/" + nazivSlike;

           
                
                return getPath;

            }
            return null;

            
        }
    }
}
