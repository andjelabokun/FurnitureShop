namespace SalonNamestaja.Application.DTOs
{
    public class StavkaPorudzbineDto
    {
        public int ProizvodID { get; set; }
        public string? ProizvodNaziv { get; set; }

        public int Kolicina { get; set; }
        public double CenaPoKomadu { get; set; }
        public double Iznos { get; set; }
    }
}