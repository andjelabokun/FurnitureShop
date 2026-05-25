using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Sto : Proizvod
    {
        public string Oblik { get; set; }
        public int BrojMesta { get; set; }
        public bool Rasklopiv { get; set; }
    }
}
