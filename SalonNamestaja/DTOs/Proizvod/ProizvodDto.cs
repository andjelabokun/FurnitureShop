namespace SalonNamestajaAPI.DTOs
{
    public class ProizvodDto
    {
        public int ProizvodID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public int StanjeNaLageru { get; set; }

        public int PodkategorijaID { get; set; }
        public int MaterijalID { get; set; }
        public int BojaID { get; set; }
        public int DimenzijeID { get; set; }
        public int ProizvodjacID { get; set; }
    }
}
