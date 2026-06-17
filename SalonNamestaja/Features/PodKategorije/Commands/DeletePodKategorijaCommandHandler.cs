using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.PodKategorije.Commands
{
    public class DeletePodKategorijaCommandHandler : IRequestHandler<DeletePodKategorijaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePodKategorijaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeletePodKategorijaCommand request, CancellationToken cancellationToken)
        {
            var podkategorija = _unitOfWork.PodKategorije.GetById(request.Id);

            if (podkategorija == null)
                return Task.FromResult(false);

            var postojiProizvod = _unitOfWork.Proizvodi
                .GetAll()
                .Any(p => p.PodkategorijaID == request.Id);

            if (postojiProizvod)
                throw new InvalidOperationException("Podkategorija ne može biti obrisana jer postoji proizvod koji joj pripada.");

            _unitOfWork.PodKategorije.Remove(podkategorija);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}