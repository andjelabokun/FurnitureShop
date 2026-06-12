using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetProizvodByIdQuery : IRequest<Proizvod?>
    {
        public int Id { get; set; }

        public GetProizvodByIdQuery(int id)
        {
            Id = id;
        }
    }
}
