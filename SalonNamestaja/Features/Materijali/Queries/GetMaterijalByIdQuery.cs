using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Materijali.Queries
{
    public class GetMaterijalByIdQuery : IRequest<Materijal?>
    {
        public int Id { get; set; }

        public GetMaterijalByIdQuery(int id)
        {
            Id = id;
        }
    }
}
