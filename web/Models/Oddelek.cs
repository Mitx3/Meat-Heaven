using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Oddelek
    {
        public int OddelekID { get; set; }
        
        [Display(Name = "Ime oddelka")]
        public string? OddelekIme { get; set; }    

        [Display(Name = "Vrsta izdelkov")]
        public string? VrstaIzdelkov { get; set; }


        public int KmetijaID { get; set; }



        public ICollection<Izdelek>? Izdelki { get; set; }

        public ICollection<Kmet>? Kmetje { get; set; }

        public ICollection<Zgradba>? Zgradbe { get; set; }


    }
}