using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Prodavci.Commands
{
    public class UpdateProdavacCommandHandler : IRequestHandler<UpdateProdavacCommand, Prodavac?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProdavacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Prodavac?> Handle(UpdateProdavacCommand request, CancellationToken cancellationToken)
        {
            var prodavac = _unitOfWork.Prodavci.GetById(request.Id);

            if (prodavac == null)
                return Task.FromResult<Prodavac?>(null);

            prodavac.Ime = request.Dto.Ime;
            prodavac.Prezime = request.Dto.Prezime;
            prodavac.KorisnickoIme = request.Dto.KorisnickoIme;

            _unitOfWork.Prodavci.Update(prodavac);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Prodavac?>(prodavac);
        }
    }
}
