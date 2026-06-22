using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Commands
{
    public class UpdateProizvodCommand : IRequest<Proizvod?>
    {
        public int Id { get; set; }
        public ProizvodUpdateDto Dto { get; set; }

        public UpdateProizvodCommand(int id, ProizvodUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
