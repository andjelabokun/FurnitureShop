namespace SalonNamestaja.Domain
{

    public enum TipKupca
    {
        FizickoLice,
        PravnoLice
    }

    public class Kupac
    {
        
            public int KupacID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public string Telefon { get; set; }
            public TipKupca TipKupca { get; set; }
            public int? PIB { get; set; } // opciono, samo za pravna lica

            public ICollection<Porudzbina> Porudzbine { get; set; }
        
    }
}
