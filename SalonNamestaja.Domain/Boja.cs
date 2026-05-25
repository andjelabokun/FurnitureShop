using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Boja
    {
        public int BojaID { get; set; }
        public string Naziv { get; set; }

        public ICollection<Proizvod> Proizvodi { get; set; }
    }
}
