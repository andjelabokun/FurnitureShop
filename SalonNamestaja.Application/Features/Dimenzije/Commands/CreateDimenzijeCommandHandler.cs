using MediatR;
using SalonNamestaja.Domain.Repositories;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestaja.Application.Features.Dimenzije.Commands
{
    public class CreateDimenzijeCommandHandler
        : IRequestHandler<CreateDimenzijeCommand, DomainDimenzije>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDimenzijeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<DomainDimenzije> Handle(
            CreateDimenzijeCommand request,
            CancellationToken cancellationToken)
        {
            var dimenzije = new DomainDimenzije
            {
                Sirina = request.Dto.Sirina,
                Visina = request.Dto.Visina,
                Dubina = request.Dto.Dubina
            };

            _unitOfWork.Dimenzije.Add(dimenzije);
            _unitOfWork.SaveChanges();

            return Task.FromResult(dimenzije);
        }
    }
}
