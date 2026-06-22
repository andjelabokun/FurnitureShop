using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Kategorije.Commands
{
    public class DeleteKategorijaCommandHandler
        : IRequestHandler<DeleteKategorijaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteKategorijaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(
            DeleteKategorijaCommand request,
            CancellationToken cancellationToken)
        {
            var kategorija = _unitOfWork.Kategorije.GetById(request.Id);

            if (kategorija == null)
                return Task.FromResult(false);

            var podkategorije = _unitOfWork.PodKategorije
                .GetAll()
                .Where(pk => pk.KategorijaID == request.Id)
                .ToList();

            var podkategorijeIds = podkategorije
                .Select(pk => pk.PodkategorijaID)
                .ToList();

            var postojiProizvod = _unitOfWork.Proizvodi
                .GetAll()
                .Any(p => podkategorijeIds.Contains(p.PodkategorijaID));

            if (postojiProizvod)
                throw new InvalidOperationException("Kategorija ne može biti obrisana jer postoji proizvod koji pripada toj kategoriji.");

            if (podkategorije.Any())
                throw new InvalidOperationException("Kategorija ne može biti obrisana jer ima podkategorije.");

            _unitOfWork.Kategorije.Remove(kategorija);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}