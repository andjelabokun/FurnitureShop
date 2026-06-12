using MediatR;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestajaAPI.Features.Dimenzije.Queries
{
    public class GetAllDimenzijeQuery : IRequest<IEnumerable<DomainDimenzije>>
    {
    }
}