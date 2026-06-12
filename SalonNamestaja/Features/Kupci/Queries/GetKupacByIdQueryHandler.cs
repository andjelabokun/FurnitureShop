using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Kupci.Queries
{
    public class GetKupacByIdQueryHandler : IRequestHandler<GetKupacByIdQuery, Kupac?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetKupacByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Kupac?> Handle(GetKupacByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Kupci.GetById(request.Id));
        }
    }
}
