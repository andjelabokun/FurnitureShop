namespace SalonNamestajaAPI.DTOs
{
    public class DostavaCreateDto
    {
        public DateTime DatumDostave { get; set; }
        public string Status { get; set; }
        public decimal CenaDostave { get; set; }
        public int PorudzbinaID { get; set; }
    }
}
