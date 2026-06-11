using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestaja.Infrastructure.Identity;

namespace SalonNamestaja.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Kupac> Kupci { get; set; }
        public DbSet<Prodavac> Prodavci { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<PodKategorija> PodKategorije { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Garnitura> Garniture { get; set; }
        public DbSet<Orman> Ormani { get; set; }
        public DbSet<Krevet> Kreveti { get; set; }
        public DbSet<Sto> Stolovi { get; set; }
        public DbSet<Materijal> Materijali { get; set; }
        public DbSet<Boja> Boje { get; set; }
        public DbSet<Dimenzije> Dimenzije { get; set; }
        public DbSet<Proizvodjac> Proizvodjaci { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<StavkaPorudzbine> StavkePorudzbine { get; set; }
        public DbSet<Dostava> Dostave { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
                

            modelBuilder.Entity<Proizvod>()
                .HasDiscriminator<string>("TipProizvoda")
                .HasValue<Proizvod>("Proizvod")
                .HasValue<Garnitura>("Garnitura")
                .HasValue<Orman>("Orman")
                .HasValue<Krevet>("Krevet")
                .HasValue<Sto>("Sto");

            modelBuilder.Entity<StavkaPorudzbine>()
                .HasKey(s => s.StavkaPorudzbinaID);

            
        }
    }
}

