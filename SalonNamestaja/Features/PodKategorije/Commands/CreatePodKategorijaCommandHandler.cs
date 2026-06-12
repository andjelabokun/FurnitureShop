using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.PodKategorije.Commands
{
    public class CreatePodKategorijaCommandHandler : IRequestHandler<CreatePodKategorijaCommand, PodKategorija>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePodKategorijaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PodKategorija> Handle(CreatePodKategorijaCommand request, CancellationToken cancellationToken)
        {
            var podkategorija = new PodKategorija
            {
                Naziv = request.Dto.Naziv,
                KategorijaID = request.Dto.KategorijaID
            };

            _unitOfWork.PodKategorije.Add(podkategorija);
            _unitOfWork.SaveChanges();

            return Task.FromResult(podkategorija);
        }
    }
}
