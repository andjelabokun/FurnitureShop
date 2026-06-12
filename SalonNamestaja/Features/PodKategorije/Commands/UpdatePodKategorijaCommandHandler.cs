using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.PodKategorije.Commands
{
    public class UpdatePodKategorijaCommandHandler : IRequestHandler<UpdatePodKategorijaCommand, PodKategorija?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePodKategorijaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PodKategorija?> Handle(UpdatePodKategorijaCommand request, CancellationToken cancellationToken)
        {
            var podkategorija = _unitOfWork.PodKategorije.GetById(request.Id);

            if (podkategorija == null)
                return Task.FromResult<PodKategorija?>(null);

            podkategorija.Naziv = request.Dto.Naziv;
            podkategorija.KategorijaID = request.Dto.KategorijaId;

            _unitOfWork.PodKategorije.Update(podkategorija);
            _unitOfWork.SaveChanges();

            return Task.FromResult<PodKategorija?>(podkategorija);
        }
    }
}