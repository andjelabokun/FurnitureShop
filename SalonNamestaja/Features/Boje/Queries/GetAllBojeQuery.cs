using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Boje.Queries
{
    public class GetAllBojeQuery : IRequest<IEnumerable<Boja>>
    {
    }
}