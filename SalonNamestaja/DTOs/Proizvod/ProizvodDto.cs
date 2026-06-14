namespace SalonNamestajaAPI.DTOs
{
    public class ProizvodDto
    {
        public int ProizvodID { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public double Cena { get; set; }
        public int StanjeNaLageru { get; set; }

        public int PodkategorijaID { get; set; }
        public int BojaID { get; set; }

        public double? Sirina { get; set; }
        public double? Visina { get; set; }
        public double? Dubina { get; set; }

        public string? SlikaUrl { get; set; }
    }
}