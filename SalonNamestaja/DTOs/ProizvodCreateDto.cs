namespace SalonNamestajaAPI.DTOs
{
    public class ProizvodCreateDto
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cena { get; set; }
        public int StanjeNaLageru { get; set; }

        public int PodkategorijaId { get; set; }
        public int MaterijalId { get; set; }
        public int BojaId { get; set; }
        public int DimenzijeId { get; set; }
        public int ProizvodjacId { get; set; }
    }
}
