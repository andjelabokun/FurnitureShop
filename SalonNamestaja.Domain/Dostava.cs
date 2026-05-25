using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Dostava
    {
        public int DostavaID { get; set; }
        public DateTime DatumDostave { get; set; }
        public string Status { get; set; }
        public double CenaDostave { get; set; }

        public int PorudzbinaID { get; set; }
        public Porudzbina Porudzbina { get; set; }
    }
}
