using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Kategorije.Commands
{
    public class CreateKategorijaCommandHandler
        : IRequestHandler<CreateKategorijaCommand, Kategorija>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateKategorijaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Kategorija> Handle(
            CreateKategorijaCommand request,
            CancellationToken cancellationToken)
        {
            var kategorija = new Kategorija
            {
                Naziv = request.Dto.Naziv,
                SlikaUrl = request.Dto.SlikaUrl
            };

            _unitOfWork.Kategorije.Add(kategorija);
            _unitOfWork.SaveChanges();

            return Task.FromResult(kategorija);
        }
    }
}