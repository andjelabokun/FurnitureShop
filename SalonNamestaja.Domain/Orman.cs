using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Orman : Proizvod
    {
        public int BrojVrata { get; set; }
        public bool ImaOgledalo { get; set; }
        public string TipVrata { get; set; }
    }
}
