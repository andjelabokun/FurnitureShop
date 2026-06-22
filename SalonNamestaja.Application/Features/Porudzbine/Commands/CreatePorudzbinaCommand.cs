using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Porudzbine.Commands
{
    public class CreatePorudzbinaCommand : IRequest<int>
    {
        public PorudzbinaCreateDto Dto { get; set; }

        public CreatePorudzbinaCommand(PorudzbinaCreateDto dto)
        {
            Dto = dto;
        }
    }
}
