using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Kupci.Commands
{
    public class UpdateKupacCommandHandler : IRequestHandler<UpdateKupacCommand, Kupac?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateKupacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Kupac?> Handle(UpdateKupacCommand request, CancellationToken cancellationToken)
        {
            var kupac = _unitOfWork.Kupci.GetById(request.Id);

            if (kupac == null)
                return Task.FromResult<Kupac?>(null);

            kupac.Ime = request.Dto.Ime;
            kupac.Prezime = request.Dto.Prezime;
            kupac.Email = request.Dto.Email;
            kupac.Telefon = request.Dto.Telefon;

            _unitOfWork.Kupci.Update(kupac);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Kupac?>(kupac);
        }
    }
}
