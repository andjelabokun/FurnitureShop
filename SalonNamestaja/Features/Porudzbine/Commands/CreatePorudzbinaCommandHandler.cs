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
            if (request.Dto.Stavke == null || !request.Dto.Stavke.Any())
                throw new Exception("Porudžbina mora imati bar jednu stavku.");

            // Prvo proveravamo da li svih proizvoda ima dovoljno na lageru
            foreach (var grupa in request.Dto.Stavke.GroupBy(s => s.ProizvodID))
            {
                var proizvod = _unitOfWork.Proizvodi.GetById(grupa.Key);

                if (proizvod == null)
                    throw new Exception("Proizvod nije pronađen.");

                var ukupnaKolicina = grupa.Sum(s => s.Kolicina);

                if (proizvod.StanjeNaLageru < ukupnaKolicina)
                    throw new Exception($"Nema dovoljno proizvoda na lageru: {proizvod.Naziv}");
            }

            var porudzbina = new Porudzbina
            {
                DatumVreme = DateTime.Now,
                Status = "Kreirana",
                UkupanIznos = request.Dto.UkupanIznos,
                ApplicationUserId = request.Dto.ApplicationUserId
            };

            _unitOfWork.Porudzbine.Add(porudzbina);
            _unitOfWork.SaveChanges();

            int rb = 1;

            foreach (var stavka in request.Dto.Stavke)
            {
                var proizvod = _unitOfWork.Proizvodi.GetById(stavka.ProizvodID);

                if (proizvod == null)
                    throw new Exception("Proizvod nije pronađen.");

                proizvod.StanjeNaLageru -= stavka.Kolicina;
                _unitOfWork.Proizvodi.Update(proizvod);

                var novaStavka = new StavkaPorudzbine
                {
                    PorudzbinaID = porudzbina.PorudzbinaID,
                    Rb = rb++,
                    ProizvodID = stavka.ProizvodID,
                    Kolicina = stavka.Kolicina,
                    CenaPoKomadu = (double)stavka.CenaPoKomadu,
                    Iznos = stavka.Kolicina * (double)stavka.CenaPoKomadu
                };

                _unitOfWork.StavkePorudzbine.Add(novaStavka);
            }

            _unitOfWork.SaveChanges();

            return Task.FromResult(porudzbina.PorudzbinaID);
        }
    }
}