using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodjaci.Commands
{
    public class CreateProizvodjacCommand : IRequest<Proizvodjac>
    {
        public ProizvodjacDto Dto { get; set; }

        public CreateProizvodjacCommand(ProizvodjacDto dto)
        {
            Dto = dto;
        }
    }
}
