namespace SalonNamestajaAPI.DTOs
{
    public class ProizvodDto
    {
        public int ProizvodID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cena { get; set; }
        public int StanjeNaLageru { get; set; }

        public int PodkategorijaID { get; set; }
        public int MaterijalID { get; set; }
        public int BojaID { get; set; }
        public int DimenzijeID { get; set; }
        public int ProizvodjacID { get; set; }

        public double? Sirina { get; set; }
        public double? Visina { get; set; }
        public double? Dubina { get; set; }
    }
}
