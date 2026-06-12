using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Kategorije.Commands
{
    public class DeleteKategorijaCommandHandler
        : IRequestHandler<DeleteKategorijaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteKategorijaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(
            DeleteKategorijaCommand request,
            CancellationToken cancellationToken)
        {
            var kategorija =
                _unitOfWork.Kategorije.GetById(request.Id);

            if (kategorija == null)
                return Task.FromResult(false);

            _unitOfWork.Kategorije.Remove(kategorija);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
