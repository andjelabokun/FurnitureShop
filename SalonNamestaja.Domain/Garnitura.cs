using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Garnitura : Proizvod
    {
        public string Punjenje { get; set; }
        public string Orijentacija { get; set; } // leva/desna
        public int BrojMesta { get; set; }
        public bool Rasklopiva { get; set; }
    }
}
