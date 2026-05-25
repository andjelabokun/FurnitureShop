using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Proizvodjac
    {
        public int ProizvodjacID { get; set; }
        public string Naziv { get; set; }
        public string Drzava { get; set; }

        public ICollection<Proizvod> Proizvodi { get; set; }
    }
}
