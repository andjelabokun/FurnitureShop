using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Boje.Commands
{
    public class CreateBojaCommand : IRequest<Boja>
    {
        public BojaDto Dto { get; set; }

        public CreateBojaCommand(BojaDto dto)
        {
            Dto = dto;
        }
    }
}
