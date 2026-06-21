using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Commands
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
