using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Kupci.Queries
{
    public class GetKupacByIdQuery : IRequest<Kupac?>
    {
        public int Id { get; set; }

        public GetKupacByIdQuery(int id)
        {
            Id = id;
        }
    }
}
