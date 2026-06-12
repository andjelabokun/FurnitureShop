using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Prodavci.Queries
{
    public class GetProdavacByIdQueryHandler : IRequestHandler<GetProdavacByIdQuery, Prodavac?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProdavacByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Prodavac?> Handle(GetProdavacByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Prodavci.GetById(request.Id));
        }
    }
}
