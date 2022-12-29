using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Kmetija
    {
        public int ID { get; set; }
        public string? Lastnik { get; set; }    
        public string? Lokacija { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public ApplicationUser? Owner { get; set; }

        public ICollection<Izdelek>? Izdelki { get; set; }
    }
}