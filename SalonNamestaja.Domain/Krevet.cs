using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Krevet : Proizvod
    {
        public string DimenzijaDuseka { get; set; }
        public bool ImaSanduk { get; set; }
        public string TipKreveta { get; set; }
    }
}
