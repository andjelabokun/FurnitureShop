namespace SalonNamestaja.Application.DTOs
{
    public class PorudzbinaCreateDto
    {
        public double UkupanIznos { get; set; }
        public string ApplicationUserId { get; set; }
        public List<StavkaPorudzbineDto> Stavke { get; set; }
    }
}
