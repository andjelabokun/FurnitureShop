using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodjaci.Commands
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
