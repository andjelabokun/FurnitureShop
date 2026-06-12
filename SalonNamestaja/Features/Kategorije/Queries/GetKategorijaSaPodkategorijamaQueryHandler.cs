using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Kategorije.Queries
{
    public class GetKategorijaSaPodkategorijamaQueryHandler
        : IRequestHandler<GetKategorijaSaPodkategorijamaQuery, Kategorija?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetKategorijaSaPodkategorijamaQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Kategorija?> Handle(
            GetKategorijaSaPodkategorijamaQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _unitOfWork.Kategorije.GetByIdSaPodkategorijama(request.Id));
        }
    }
}
