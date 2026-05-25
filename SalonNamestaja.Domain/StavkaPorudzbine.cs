using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class StavkaPorudzbine
    {
        public int StavkaPorudzbinaID { get; set; }
        public int Rb { get; set; }
        public int Kolicina { get; set; }
        public double CenaPoKomadu { get; set; }
        public double Iznos { get; set; }

        public int PorudzbinaID { get; set; }
        public Porudzbina Porudzbina { get; set; }

        public int ProizvodID { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
