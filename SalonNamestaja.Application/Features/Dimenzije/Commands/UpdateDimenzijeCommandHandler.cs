using MediatR;
using SalonNamestaja.Domain.Repositories;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestaja.Application.Features.Dimenzije.Commands
{
    public class UpdateDimenzijeCommandHandler
        : IRequestHandler<UpdateDimenzijeCommand, DomainDimenzije?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDimenzijeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<DomainDimenzije?> Handle(
            UpdateDimenzijeCommand request,
            CancellationToken cancellationToken)
        {
            var dimenzije = _unitOfWork.Dimenzije.GetById(request.Id);

            if (dimenzije == null)
                return Task.FromResult<DomainDimenzije?>(null);

            dimenzije.Sirina = request.Dto.Sirina;
            dimenzije.Visina = request.Dto.Visina;
            dimenzije.Dubina = request.Dto.Dubina;

            _unitOfWork.Dimenzije.Update(dimenzije);
            _unitOfWork.SaveChanges();

            return Task.FromResult<DomainDimenzije?>(dimenzije);
        }
    }
}
