namespace SalonNamestajaAPI.DTOs
{
    public class ProizvodCreateDto
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
    }
}

