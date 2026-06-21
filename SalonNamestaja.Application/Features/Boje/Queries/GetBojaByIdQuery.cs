using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestaja.Application.Features.Boje.Queries
{
    public class GetBojaByIdQuery : IRequest<Boja?>
    {
        public int Id { get; set; }

        public GetBojaByIdQuery(int id)
        {
            Id = id;
        }
    }
}
