using MediatR;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestaja.Application.Features.Dimenzije.Queries
{
    public class GetAllDimenzijeQuery : IRequest<IEnumerable<DomainDimenzije>>
    {
    }
}