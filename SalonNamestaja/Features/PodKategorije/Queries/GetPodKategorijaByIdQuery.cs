using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.PodKategorije.Queries
{
    public class GetPodKategorijaByIdQuery : IRequest<PodKategorija?>
    {
        public int Id { get; set; }

        public GetPodKategorijaByIdQuery(int id)
        {
            Id = id;
        }
    }
}
