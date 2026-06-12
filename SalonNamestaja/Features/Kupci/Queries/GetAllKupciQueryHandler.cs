using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Kupci.Queries
{
    public class GetAllKupciQueryHandler : IRequestHandler<GetAllKupciQuery, IEnumerable<Kupac>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllKupciQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Kupac>> Handle(GetAllKupciQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Kupci.GetAll());
        }
    }
}
