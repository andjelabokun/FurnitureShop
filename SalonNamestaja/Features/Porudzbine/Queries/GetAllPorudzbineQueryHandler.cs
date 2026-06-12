using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Porudzbine.Queries
{
    public class GetAllPorudzbineQueryHandler : IRequestHandler<GetAllPorudzbineQuery, IEnumerable<Porudzbina>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPorudzbineQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Porudzbina>> Handle(GetAllPorudzbineQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Porudzbine.GetAll());
        }
    }
}