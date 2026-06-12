using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Prodavci.Queries
{
    public class GetAllProdavciQueryHandler : IRequestHandler<GetAllProdavciQuery, IEnumerable<Prodavac>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProdavciQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Prodavac>> Handle(GetAllProdavciQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Prodavci.GetAll());
        }
    }
}
