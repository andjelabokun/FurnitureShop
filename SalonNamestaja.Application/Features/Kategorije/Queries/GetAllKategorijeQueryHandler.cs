using MediatR;
using SalonNamestaja.Domain.Repositories;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Kategorije.Queries
{
    public class GetAllKategorijeQueryHandler
        : IRequestHandler<GetAllKategorijeQuery, IEnumerable<KategorijaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllKategorijeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<KategorijaDto>> Handle(
            GetAllKategorijeQuery request,
            CancellationToken cancellationToken)
        {
            var kategorije = _unitOfWork.Kategorije.GetAll()
                .Select(k => new KategorijaDto
                {
                    KategorijaID = k.KategorijaID,
                    Naziv = k.Naziv,
                    SlikaUrl = k.SlikaUrl
                });

            return Task.FromResult(kategorije);
        }
    }
}