using SalonNamestaja.Domain;

namespace SalonNamestaja.Domain.Repositories
{
    public interface IPorudzbinaRepository : IRepository<Porudzbina>
    {
        IEnumerable<Porudzbina> GetAllSaStavkama();
    }
}
