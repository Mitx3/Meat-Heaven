namespace web.Models
{


    public class Izdelek
    {
        public int IzdelekID { get; set; }

        public string IzdelekIme { get; set; }

        public string IzdelekVrsta { get; set; }

        public double IzdelekCena { get; set; }

        public DateTime RokNakupa { get; set; }

        public Kmetija Kmetija { get; set; }
    }
}