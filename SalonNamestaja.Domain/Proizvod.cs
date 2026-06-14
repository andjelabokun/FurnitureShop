using System.Collections.Generic;

namespace SalonNamestaja.Domain
{
    public class Proizvod
    {
        public int ProizvodID { get; set; }

        public string Naziv { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public double Cena { get; set; }
        public int StanjeNaLageru { get; set; }

        public int DimenzijeID { get; set; }
        public Dimenzije Dimenzije { get; set; } = null!;

        public int PodkategorijaID { get; set; }
        public PodKategorija Podkategorija { get; set; } = null!;

        public int MaterijalID { get; set; }
        public Materijal Materijal { get; set; } = null!;

        public int BojaID { get; set; }
        public Boja Boja { get; set; } = null!;

        public int ProizvodjacID { get; set; }
        public Proizvodjac Proizvodjac { get; set; } = null!;

        public string? SlikaUrl { get; set; }

        public string TipProizvoda { get; set; } = "Proizvod";

        public ICollection<StavkaPorudzbine> StavkePorudzbine { get; set; } = new List<StavkaPorudzbine>();
    }
}