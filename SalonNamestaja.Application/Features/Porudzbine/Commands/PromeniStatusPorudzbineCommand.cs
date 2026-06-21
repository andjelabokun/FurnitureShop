using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestaja.Application.Features.Porudzbine.Commands
{
    public class PromeniStatusPorudzbineCommand : IRequest<Porudzbina?>
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public PromeniStatusPorudzbineCommand(int id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}
