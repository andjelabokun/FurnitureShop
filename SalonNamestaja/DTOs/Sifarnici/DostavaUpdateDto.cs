namespace SalonNamestajaAPI.DTOs
{
    public class DostavaUpdateDto
    {
        public DateTime DatumDostave { get; set; }
        public string Status { get; set; } = string.Empty;
        public double CenaDostave { get; set; }
    }
}
