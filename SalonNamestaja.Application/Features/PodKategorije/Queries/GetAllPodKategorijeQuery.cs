using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestaja.Application.Features.PodKategorije.Queries
{
    public class GetAllPodKategorijeQuery : IRequest<IEnumerable<PodKategorija>>
    {
    }
}
