using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Prodavci.Commands
{
    public class CreateProdavacCommandHandler : IRequestHandler<CreateProdavacCommand, Prodavac>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProdavacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Prodavac> Handle(CreateProdavacCommand request, CancellationToken cancellationToken)
        {
            var prodavac = new Prodavac
            {
                Ime = request.Dto.Ime,
                Prezime = request.Dto.Prezime,
                KorisnickoIme = request.Dto.KorisnickoIme,
                Lozinka = request.Dto.Lozinka
            };

            _unitOfWork.Prodavci.Add(prodavac);
            _unitOfWork.SaveChanges();

            return Task.FromResult(prodavac);
        }
    }
}
