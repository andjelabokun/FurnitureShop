using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Commands
{
    public class CreateProizvodCommand : IRequest<Proizvod>
    {
        public ProizvodCreateDto Dto { get; set; }

        public CreateProizvodCommand(ProizvodCreateDto dto)
        {
            Dto = dto;
        }
    }
}
