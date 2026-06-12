using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetAllProizvodiQueryHandler : IRequestHandler<GetAllProizvodiQuery, IEnumerable<Proizvod>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProizvodiQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Proizvod>> Handle(GetAllProizvodiQuery request, CancellationToken cancellationToken)
        {
            var proizvodi = _unitOfWork.Proizvodi.GetAll();
            return Task.FromResult(proizvodi);
        }
    }
}
