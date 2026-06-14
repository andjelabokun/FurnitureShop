namespace SalonNamestajaAPI.DTOs
{
    public class KategorijaDto
    {
        public int KategorijaID { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string? SlikaUrl { get; set; }
    }
}