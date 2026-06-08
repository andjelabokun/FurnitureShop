namespace SalonNamestajaAPI.DTOs
{
    public class StavkaPorudzbineDto
    {
        public int StavkaPorudzbineID { get; set; }
        public int ProizvodID { get; set; }
        public int Kolicina { get; set; }
        public decimal CenaPoKomadu { get; set; }
        public decimal Iznos { get; set; }
    }
}
