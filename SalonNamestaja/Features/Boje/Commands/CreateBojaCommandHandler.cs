using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Boje.Commands
{
    public class CreateBojaCommandHandler : IRequestHandler<CreateBojaCommand, Boja>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBojaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Boja> Handle(CreateBojaCommand request, CancellationToken cancellationToken)
        {
            var boja = new Boja
            {
                Naziv = request.Dto.Naziv
            };

            _unitOfWork.Boje.Add(boja);
            _unitOfWork.SaveChanges();

            return Task.FromResult(boja);
        }
    }
}
