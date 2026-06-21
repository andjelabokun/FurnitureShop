using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Proizvodjaci.Queries
{
    public class GetProizvodjacByIdQueryHandler : IRequestHandler<GetProizvodjacByIdQuery, Proizvodjac?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProizvodjacByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Proizvodjac?> Handle(GetProizvodjacByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Proizvodjaci.GetById(request.Id));
        }
    }
}
