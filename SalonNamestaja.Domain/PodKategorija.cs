namespace SalonNamestaja.Domain
{
    public class PodKategorija
    {
        public int PodkategorijaID { get; set; }
        public string Naziv { get; set; }

        public int KategorijaID { get; set; }
        public Kategorija Kategorija { get; set; }

        public ICollection<Proizvod> Proizvodi { get; set; }
    }
}
