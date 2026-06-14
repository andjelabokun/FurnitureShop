namespace SalonNamestajaAPI.DTOs
{
    public class ProizvodUpdateDto
    {
        public string Naziv { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public double Cena { get; set; }
        public int StanjeNaLageru { get; set; }

        public int PodkategorijaId { get; set; }
        public int MaterijalId { get; set; }
        public int BojaId { get; set; }
        public int DimenzijeId { get; set; }
        public int ProizvodjacId { get; set; }

        public string? SlikaUrl { get; set; }

        // Specijalizacija proizvoda
        public string TipProizvoda { get; set; } = "Proizvod";

        // Garnitura
        public string? Punjenje { get; set; }
        public string? Orijentacija { get; set; }
        public int? BrojMesta { get; set; }
        public bool? Rasklopiva { get; set; }

        // Krevet
        public string? DimenzijaDuseka { get; set; }
        public bool? ImaSanduk { get; set; }
        public string? TipKreveta { get; set; }

        // Orman
        public int? BrojVrata { get; set; }
        public bool? ImaOgledalo { get; set; }
        public string? TipVrata { get; set; }

        // Sto
        public string? Oblik { get; set; }
        public bool? Rasklopiv { get; set; }
    }
}