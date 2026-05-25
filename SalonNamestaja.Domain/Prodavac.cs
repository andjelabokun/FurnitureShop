namespace SalonNamestaja.Domain
{
    public class Prodavac
    {
        
            public int ProdavacID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string KorisnickoIme { get; set; }
            public string Lozinka { get; set; }

            public ICollection<Porudzbina> Porudzbine { get; set; }
        
    }
}
