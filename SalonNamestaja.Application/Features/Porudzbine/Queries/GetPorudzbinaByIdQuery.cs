using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestaja.Application.Features.Porudzbine.Queries
{
    public class GetPorudzbinaByIdQuery : IRequest<Porudzbina?>
    {
        public int Id { get; set; }

        public GetPorudzbinaByIdQuery(int id)
        {
            Id = id;
        }
    }
}
