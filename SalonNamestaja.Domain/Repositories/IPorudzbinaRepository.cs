using SalonNamestaja.Domain;

namespace SalonNamestaja.Domain.Repositories
{
    public interface IPorudzbinaRepository : IRepository<Porudzbina>
    {
        IEnumerable<Porudzbina> GetAllSaStavkama();

        IEnumerable<Porudzbina> GetFiltriranePorudzbine(
                                      string? pretraga,
                                      string? status,
                                      DateTime? datumOd,
                                      DateTime? datumDo);
    }
}
