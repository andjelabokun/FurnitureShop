using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Kategorije.Queries
{
    public class GetKategorijaByIdQueryHandler
        : IRequestHandler<GetKategorijaByIdQuery, Kategorija?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetKategorijaByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Kategorija?> Handle(
            GetKategorijaByIdQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _unitOfWork.Kategorije.GetById(request.Id));
        }
    }
}
