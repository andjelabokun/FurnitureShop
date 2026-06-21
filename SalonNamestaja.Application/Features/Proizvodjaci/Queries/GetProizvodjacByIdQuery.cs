using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestaja.Application.Features.Proizvodjaci.Queries
{
    public class GetProizvodjacByIdQuery : IRequest<Proizvodjac?>
    {
        public int Id { get; set; }

        public GetProizvodjacByIdQuery(int id)
        {
            Id = id;
        }
    }
}
