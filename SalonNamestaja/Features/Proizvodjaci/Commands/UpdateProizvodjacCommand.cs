using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodjaci.Commands
{
    public class UpdateProizvodjacCommand : IRequest<Proizvodjac?>
    {
        public int Id { get; set; }
        public ProizvodjacDto Dto { get; set; }

        public UpdateProizvodjacCommand(int id, ProizvodjacDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
