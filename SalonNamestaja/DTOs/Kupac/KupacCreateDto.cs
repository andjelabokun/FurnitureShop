using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.DTOs
{
    public class KupacCreateDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public TipKupca TipKupca { get; set; }  
        public int? PIB { get; set; }
    }
}
