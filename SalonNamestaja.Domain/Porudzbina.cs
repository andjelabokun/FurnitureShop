using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Porudzbina
    {
        public int PorudzbinaID { get; set; }
        public DateTime DatumVreme { get; set; }
        public string Status { get; set; }
        public double UkupanIznos { get; set; }

        public string ApplicationUserId { get; set; }   

        public ICollection<StavkaPorudzbine> StavkePorudzbine { get; set; }
        public Dostava Dostava { get; set; }
    }
}
