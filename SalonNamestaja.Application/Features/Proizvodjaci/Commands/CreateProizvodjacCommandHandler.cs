using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Proizvodjaci.Commands
{
    public class CreateProizvodjacCommandHandler : IRequestHandler<CreateProizvodjacCommand, Proizvodjac>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProizvodjacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Proizvodjac> Handle(CreateProizvodjacCommand request, CancellationToken cancellationToken)
        {
            var proizvodjac = new Proizvodjac
            {
                Naziv = request.Dto.Naziv,
                Drzava = request.Dto.Drzava
            };

            _unitOfWork.Proizvodjaci.Add(proizvodjac);
            _unitOfWork.SaveChanges();

            return Task.FromResult(proizvodjac);
        }
    }
}
