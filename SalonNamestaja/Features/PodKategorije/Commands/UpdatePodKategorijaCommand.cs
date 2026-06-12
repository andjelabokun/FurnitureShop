using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.PodKategorije.Commands
{
    public class UpdatePodKategorijaCommand : IRequest<PodKategorija?>
    {
        public int Id { get; set; }
        public PodkategorijaUpdateDto Dto { get; set; }

        public UpdatePodKategorijaCommand(int id, PodkategorijaUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
