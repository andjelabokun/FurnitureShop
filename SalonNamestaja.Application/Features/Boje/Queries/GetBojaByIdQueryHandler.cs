using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Boje.Queries
{
    public class GetBojaByIdQueryHandler : IRequestHandler<GetBojaByIdQuery, Boja?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBojaByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Boja?> Handle(GetBojaByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Boje.GetById(request.Id));
        }
    }
}
