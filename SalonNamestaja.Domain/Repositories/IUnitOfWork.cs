using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IKategorijaRepository Kategorije { get; }
        IProizvodRepository Proizvodi { get; }
        IRepository<PodKategorija> PodKategorije { get; }
        IRepository<Boja> Boje { get; }
        IRepository<Materijal> Materijali { get; }
        IRepository<Proizvodjac> Proizvodjaci { get; }
        IRepository<Porudzbina> Porudzbine { get; }
        IRepository<Dimenzije> Dimenzije { get; }
        IRepository<StavkaPorudzbine> StavkePorudzbine { get; }

        int SaveChanges();
    }
}
