using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Prodavci.Queries
{
    public class GetProdavacByIdQuery : IRequest<Prodavac?>
    {
        public int Id { get; set; }

        public GetProdavacByIdQuery(int id)
        {
            Id = id;
        }
    }
}
