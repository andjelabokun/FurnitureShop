using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Dimenzije
    {
        public int DimenzijeID { get; set; }
        public double Sirina { get; set; }
        public double Visina { get; set; }
        public double Dubina { get; set; }

        public int ProizvodID { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
