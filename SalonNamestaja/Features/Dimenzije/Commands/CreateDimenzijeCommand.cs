using MediatR;
using SalonNamestajaAPI.DTOs;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestajaAPI.Features.Dimenzije.Commands
{
    public class CreateDimenzijeCommand : IRequest<DomainDimenzije>
    {
        public DimenzijeDto Dto { get; set; }

        public CreateDimenzijeCommand(DimenzijeDto dto)
        {
            Dto = dto;
        }
    }
}
