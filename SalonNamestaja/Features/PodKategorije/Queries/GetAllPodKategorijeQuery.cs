using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.PodKategorije.Queries
{
    public class GetAllPodKategorijeQuery : IRequest<IEnumerable<PodKategorija>>
    {
    }
}
