using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Kategorije.Commands
{
    public class UpdateKategorijaCommandHandler
        : IRequestHandler<UpdateKategorijaCommand, Kategorija?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateKategorijaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Kategorija?> Handle(
            UpdateKategorijaCommand request,
            CancellationToken cancellationToken)
        {
            var kategorija = _unitOfWork.Kategorije.GetById(request.Id);

            if (kategorija == null)
                return Task.FromResult<Kategorija?>(null);

            kategorija.Naziv = request.Dto.Naziv;
            kategorija.SlikaUrl = request.Dto.SlikaUrl;

            _unitOfWork.Kategorije.Update(kategorija);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Kategorija?>(kategorija);
        }
    }
}