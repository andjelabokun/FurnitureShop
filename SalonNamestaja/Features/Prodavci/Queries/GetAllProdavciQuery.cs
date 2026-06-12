using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Prodavci.Queries
{
    public class GetAllProdavciQuery : IRequest<IEnumerable<Prodavac>>
    {
    }
}
