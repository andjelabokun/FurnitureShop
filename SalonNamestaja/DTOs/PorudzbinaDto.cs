namespace SalonNamestajaAPI.DTOs
{
    public class PorudzbinaDto
    {
        public int PorudzbinaID { get; set; }
        public DateTime DatumVreme { get; set; }
        public string Status { get; set; }
        public decimal UkupanIznos { get; set; }

        public int KupacID { get; set; }
        public int ProdavacID { get; set; }

        public List<StavkaPorudzbineDto> Stavke { get; set; }
    }
}
