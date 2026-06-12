using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetProizvodiPoBojiQueryHandler : IRequestHandler<GetProizvodiPoBojiQuery, IEnumerable<Proizvod>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProizvodiPoBojiQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Proizvod>> Handle(GetProizvodiPoBojiQuery request, CancellationToken cancellationToken)
        {
            var proizvodi = _unitOfWork.Proizvodi.GetSviBojom(request.BojaId);
            return Task.FromResult(proizvodi);
        }
    }
}
