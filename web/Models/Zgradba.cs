using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Zgradba
    {
        public int ZgradbaID { get; set; }

        [Display(Name = "Lokacija zgradbe")]
        public string? Lokacija { get; set; }    
 



        public int OddelekID { get; set; } 

        public Oddelek Oddelek { get; set; }


    }
}