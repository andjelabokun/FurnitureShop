using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Proizvodjaci.Queries
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
