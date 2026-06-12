using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetProizvodByIdQueryHandler : IRequestHandler<GetProizvodByIdQuery, Proizvod?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProizvodByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Proizvod?> Handle(GetProizvodByIdQuery request, CancellationToken cancellationToken)
        {
            var proizvod = _unitOfWork.Proizvodi.GetById(request.Id);
            return Task.FromResult(proizvod);
        }
    }
}
