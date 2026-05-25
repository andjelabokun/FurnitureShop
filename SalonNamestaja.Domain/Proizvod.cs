using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain
{
    public class Proizvod
    {
        public int ProizvodID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cena { get; set; }
        public int StanjeNaLageru { get; set; }
        public int DimenzijeID { get; set; }
        public Dimenzije Dimenzije { get; set; }

        public int PodkategorijaID { get; set; }
        public PodKategorija Podkategorija { get; set; }

        public int MaterijalID { get; set; }
        public Materijal Materijal { get; set; }

        public int BojaID { get; set; }
        public Boja Boja { get; set; }

        public int ProizvodjacID { get; set; }
        public Proizvodjac Proizvodjac { get; set; }

        public ICollection<StavkaPorudzbine> StavkePorudzbine { get; set; }
    }
}
