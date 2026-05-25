namespace SalonNamestajaAPI.DTOs
{
    public class PorudzbinaCreateDto
    {
        public int KupacID { get; set; }
        public int ProdavacID { get; set; }

        public List<StavkaPorudzbineCreateDto> Stavke { get; set; }
    }
}
