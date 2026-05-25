namespace SalonNamestaja.Domain
{
    public class Materijal
    {
        public int MaterijalID { get; set; }
        public string Naziv { get; set; }
        public string Tip { get; set; }

        public ICollection<Proizvod> Proizvodi { get; set; }
    }
}
