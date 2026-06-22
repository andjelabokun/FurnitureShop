using MediatR;
using SalonNamestaja.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
{
    public class GetProizvodiPoPodkategorijiQuery : IRequest<IEnumerable<ProizvodDto>>
    {
        public int PodkategorijaId { get; set; }

        public GetProizvodiPoPodkategorijiQuery(int podkategorijaId)
        {
            PodkategorijaId=podkategorijaId;
        }
    }
}
