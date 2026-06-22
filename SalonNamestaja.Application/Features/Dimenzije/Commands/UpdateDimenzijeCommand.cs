using MediatR;
using SalonNamestaja.Application.DTOs;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestaja.Application.Features.Dimenzije.Commands
{
    public class UpdateDimenzijeCommand : IRequest<DomainDimenzije?>
    {
        public int Id { get; set; }
        public DimenzijeDto Dto { get; set; }

        public UpdateDimenzijeCommand(int id, DimenzijeDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
