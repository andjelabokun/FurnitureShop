using MediatR;
using SalonNamestaja.Application.DTOs;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestaja.Application.Features.Dimenzije.Commands
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
