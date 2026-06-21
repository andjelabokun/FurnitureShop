using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Boje.Commands
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
