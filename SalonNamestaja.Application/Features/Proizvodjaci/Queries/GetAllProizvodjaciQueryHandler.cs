using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Proizvodjaci.Queries
{
    public class GetAllProizvodjaciQueryHandler : IRequestHandler<GetAllProizvodjaciQuery, IEnumerable<Proizvodjac>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProizvodjaciQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Proizvodjac>> Handle(GetAllProizvodjaciQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Proizvodjaci.GetAll());
        }
    }
}
