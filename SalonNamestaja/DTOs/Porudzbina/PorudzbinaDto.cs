namespace SalonNamestajaAPI.DTOs
{
    public class PorudzbinaDto
    {
        public int PorudzbinaID { get; set; }
        public DateTime DatumVreme { get; set; }
        public string Status { get; set; } = string.Empty;
        public double UkupanIznos { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        public string? KupacIme { get; set; }
        public string? KupacPrezime { get; set; }
        public string? KupacEmail { get; set; }
        public string? KupacTelefon { get; set; }
        public string? Adresa { get; set; }

        public List<StavkaPorudzbineDto> Stavke { get; set; } = new();
    }
}