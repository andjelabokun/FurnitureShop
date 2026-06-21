using MediatR;
using SalonNamestaja.Domain.Repositories;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestaja.Application.Features.Dimenzije.Queries
{
    public class GetDimenzijeByIdQueryHandler
        : IRequestHandler<GetDimenzijeByIdQuery, DomainDimenzije?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDimenzijeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<DomainDimenzije?> Handle(
            GetDimenzijeByIdQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Dimenzije.GetById(request.Id));
        }
    }
}
