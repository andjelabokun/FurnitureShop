using MediatR;

namespace SalonNamestajaAPI.Features.PodKategorije.Commands
{
    public class DeletePodKategorijaCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeletePodKategorijaCommand(int id)
        {
            Id = id;
        }
    }
}
