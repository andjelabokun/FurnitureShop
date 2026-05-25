using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.DTOs
{
    public class ProdavacCreateDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

    }
}
