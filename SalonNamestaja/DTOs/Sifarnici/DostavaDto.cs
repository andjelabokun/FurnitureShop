namespace SalonNamestajaAPI.DTOs
{
    public class DostavaDto
    {
        public int DostavaID { get; set; }
        public DateTime DatumDostave { get; set; }
        public string Status { get; set; }
        public decimal CenaDostave { get; set; }
        public int PorudzbinaID { get; set; }
    }
}
