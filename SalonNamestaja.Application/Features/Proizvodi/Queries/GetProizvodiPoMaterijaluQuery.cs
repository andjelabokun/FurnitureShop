using MediatR;
using SalonNamestaja.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
{
    public class GetProizvodiPoMaterijaluQuery : IRequest<IEnumerable<ProizvodDto>>
    {
        public int MaterijalId { get; set; }

        public GetProizvodiPoMaterijaluQuery(int materijalId)
        {
            MaterijalId = materijalId;
        }
    }
}
