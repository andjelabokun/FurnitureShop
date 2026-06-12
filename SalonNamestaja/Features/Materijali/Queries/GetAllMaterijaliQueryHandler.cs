using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Materijali.Queries
{
    public class GetAllMaterijaliQueryHandler : IRequestHandler<GetAllMaterijaliQuery, IEnumerable<Materijal>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMaterijaliQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Materijal>> Handle(GetAllMaterijaliQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Materijali.GetAll());
        }
    }
}
