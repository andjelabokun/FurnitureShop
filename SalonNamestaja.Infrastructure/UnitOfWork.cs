using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestaja.Infrastructure.Repositories;
using SalonNamestaja.Infrastructure.Data;

namespace SalonNamestaja.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IKategorijaRepository? _kategorije;
        private IProizvodRepository? _proizvodi;
        private IRepository<Kupac>? _kupci;
        private IRepository<Prodavac>? _prodavci;
        private IRepository<PodKategorija>? _podKategorije;
        private IRepository<Boja>? _boje;
        private IRepository<Materijal>? _materijali;
        private IRepository<Proizvodjac>? _proizvodjaci;
        private IRepository<Porudzbina>? _porudzbine;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IKategorijaRepository Kategorije =>
            _kategorije ??= new KategorijaRepository(_context);

        public IProizvodRepository Proizvodi =>
            _proizvodi ??= new ProizvodRepository(_context);

        public IRepository<Kupac> Kupci =>
            _kupci ??= new Repository<Kupac>(_context);

        public IRepository<Prodavac> Prodavci =>
            _prodavci ??= new Repository<Prodavac>(_context);

        public IRepository<PodKategorija> PodKategorije =>
            _podKategorije ??= new Repository<PodKategorija>(_context);

        public IRepository<Boja> Boje =>
            _boje ??= new Repository<Boja>(_context);

        public IRepository<Materijal> Materijali =>
            _materijali ??= new Repository<Materijal>(_context);

        public IRepository<Proizvodjac> Proizvodjaci =>
            _proizvodjaci ??= new Repository<Proizvodjac>(_context);

        public IRepository<Porudzbina> Porudzbine =>
            _porudzbine ??= new Repository<Porudzbina>(_context);

        public int SaveChanges() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
    
}
