using MediatR;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestaja.Application.Features.Dimenzije.Queries
{
    public class GetDimenzijeByIdQuery : IRequest<DomainDimenzije?>
    {
        public int Id { get; set; }

        public GetDimenzijeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
