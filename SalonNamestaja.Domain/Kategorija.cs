namespace SalonNamestaja.Domain
{
    public class Kategorija
    {
        public int KategorijaID { get; set; }
        public string Naziv { get; set; }

        public ICollection<PodKategorija> Podkategorije { get; set; }
    }
}
