using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.PodKategorije.Commands
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
