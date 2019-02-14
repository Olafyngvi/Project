using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoServis.ViewModels
{
    public class LoginVM
    {
        //[StringLength(100, ErrorMessage ="Korisničko ime mora sadržavati minimalno 3 karaktera.",MinimumLength =5)]
        public string username { get; set; }
        //[StringLength(100, ErrorMessage = "Password mora sadržavati minimalno 5 karaktera.", MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public bool ZapamtiPassword { get; set; }
    }
}
