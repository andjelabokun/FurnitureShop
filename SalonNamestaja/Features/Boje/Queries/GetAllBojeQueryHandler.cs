using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Boje.Queries
{
    public class GetAllBojeQueryHandler : IRequestHandler<GetAllBojeQuery, IEnumerable<Boja>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBojeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Boja>> Handle(GetAllBojeQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Boje.GetAll());
        }
    }
}
