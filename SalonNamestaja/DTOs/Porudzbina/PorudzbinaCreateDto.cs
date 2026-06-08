namespace SalonNamestajaAPI.DTOs
{
    public class PorudzbinaCreateDto
    {
        public DateTime DatumVreme { get; set; }
        public string Status { get; set; }
        public double UkupanIznos { get; set; }

        public int KupacID { get; set; }
        public int ProdavacID { get; set; }

        public List<StavkaPorudzbineDto> Stavke { get; set; }
    }
}
