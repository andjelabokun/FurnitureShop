using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.PodKategorije.Commands
{
    public class CreatePodKategorijaCommand : IRequest<PodKategorija>
    {
        public PodkategorijaCreateDto Dto { get; set; }

        public CreatePodKategorijaCommand(PodkategorijaCreateDto dto)
        {
            Dto = dto;
        }
    }
}
