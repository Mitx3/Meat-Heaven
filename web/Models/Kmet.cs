using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Kmet
    {
        public int KmetID { get; set; }
        public string? Ime { get; set; }    
        public string? Priimek { get; set; }
        public int? Starost { get; set; }    


        public int OddelekID { get; set; } 


        public ICollection<Oddelek>? Oddelki { get; set; }


    }
}