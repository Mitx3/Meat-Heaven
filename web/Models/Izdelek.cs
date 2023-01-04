using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace web.Models
{


    public class Izdelek
    {
        public int IzdelekID { get; set; }

        [Display(Name = "Ime izdelka")]
        public string? IzdelekIme { get; set; }

        [Display(Name = "Vrsta izdelka")]
        public string? IzdelekVrsta { get; set; }

        [Display(Name = "Cena izdelka")]
        public double? IzdelekCena { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Rok proizvodnje")]
        public DateTime? RokProizvodnje { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Rok uporabe")]
        public DateTime? RokUporabe { get; set; }




        public int OddelekID { get; set; } 

        public Oddelek? Oddelek { get; set; }
        
    }
}