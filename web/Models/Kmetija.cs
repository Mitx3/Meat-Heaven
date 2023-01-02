using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Kmetija
    {
        public int ID { get; set; }
        
        [Display(Name = "Ime lastnika")]
        public string? Lastnik { get; set; }    
        public string? Lokacija { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public ApplicationUser? Owner { get; set; }

        public ICollection<Oddelek>? Oddelki { get; set; }
    }
}