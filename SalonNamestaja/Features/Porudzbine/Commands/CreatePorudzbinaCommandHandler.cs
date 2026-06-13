using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Porudzbine.Commands
{
    public class CreatePorudzbinaCommandHandler : IRequestHandler<CreatePorudzbinaCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePorudzbinaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(CreatePorudzbinaCommand request, CancellationToken cancellationToken)
        {
            var porudzbina = new Porudzbina
            {
                DatumVreme = DateTime.Now,
                Status = "Kreirana",
                UkupanIznos = request.Dto.UkupanIznos,
                ApplicationUserId = request.Dto.ApplicationUserId
            };

            _unitOfWork.Porudzbine.Add(porudzbina);
            _unitOfWork.SaveChanges();

            if (request.Dto.Stavke != null)
            {
                foreach (var stavka in request.Dto.Stavke)
                {
                    var novaStavka = new StavkaPorudzbine
                    {
                        PorudzbinaID = porudzbina.PorudzbinaID,
                        ProizvodID = stavka.ProizvodID,
                        Kolicina = stavka.Kolicina,
                        CenaPoKomadu = (double)stavka.CenaPoKomadu,
                        Iznos = stavka.Kolicina * (double)stavka.CenaPoKomadu
                    };
                    _unitOfWork.StavkePorudzbine.Add(novaStavka);
                }
                _unitOfWork.SaveChanges();
            }

            return Task.FromResult(porudzbina.PorudzbinaID);
        }
    }
}
