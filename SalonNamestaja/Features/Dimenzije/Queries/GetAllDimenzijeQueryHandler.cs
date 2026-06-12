using MediatR;
using SalonNamestaja.Domain.Repositories;
using DomainDimenzije = SalonNamestaja.Domain.Dimenzije;

namespace SalonNamestajaAPI.Features.Dimenzije.Queries
{
    public class GetAllDimenzijeQueryHandler
        : IRequestHandler<GetAllDimenzijeQuery, IEnumerable<DomainDimenzije>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDimenzijeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<DomainDimenzije>> Handle(
            GetAllDimenzijeQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Dimenzije.GetAll());
        }
    }
}
