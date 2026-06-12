using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Kupci.Commands
{
    public class CreateKupacCommandHandler : IRequestHandler<CreateKupacCommand, Kupac>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateKupacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Kupac> Handle(CreateKupacCommand request, CancellationToken cancellationToken)
        {
            var kupac = new Kupac
            {
                Ime = request.Dto.Ime,
                Prezime = request.Dto.Prezime,
                Email = request.Dto.Email,
                Telefon = request.Dto.Telefon,
                TipKupca = request.Dto.TipKupca,
                PIB = request.Dto.PIB
            };

            _unitOfWork.Kupci.Add(kupac);
            _unitOfWork.SaveChanges();

            return Task.FromResult(kupac);
        }
    }
}
