using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Application.DTOs;
using SalonNamestaja.Application.Features.Proizvodi.Commands;
using SalonNamestaja.Application.Features.Proizvodi.Queries;

namespace SalonNamestajaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProizvodiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proizvodi = await _mediator.Send(new GetAllProizvodiQuery());
            return Ok(proizvodi);
        }

        [HttpGet("sa-dimenzijama")]
        public async Task<IActionResult> GetAllSaDimenzijama()
        {
            var proizvodi = await _mediator.Send(new GetAllProizvodiSaDimenzijamaQuery());
            return Ok(proizvodi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proizvod = await _mediator.Send(new GetProizvodByIdQuery(id));

            if (proizvod == null)
                return NotFound("Proizvod nije pronađen.");

            return Ok(proizvod);
        }

        [HttpGet("boja/{bojaId}")]
        public async Task<IActionResult> GetPoBoji(int bojaId)
        {
            var proizvodi = await _mediator.Send(new GetProizvodiPoBojiQuery(bojaId));
            return Ok(proizvodi);
        }

        [HttpGet("materijal/{materijalId}")]
        public async Task<IActionResult> GetPoMaterijalu(int materijalId)
        {
            var proizvodi = await _mediator.Send(new GetProizvodiPoMaterijaluQuery(materijalId));
            return Ok(proizvodi);
        }

        [HttpGet("podkategorija/{podKategorijaId}")]
        public async Task<IActionResult> GetPoPodKategoriji(int podKategorijaId)
        {
            var proizvodi = await _mediator.Send(new GetProizvodiPoPodkategorijiQuery(podKategorijaId));
            return Ok(proizvodi);
        }

        [HttpGet("kategorija/{kategorijaId}")]
        public async Task<IActionResult> GetPoKategoriji(int kategorijaId)
        {
            var proizvodi = await _mediator.Send(new GetProizvodiPoKategorijiQuery(kategorijaId));
            return Ok(proizvodi);
        }

        [HttpGet("cena/max/{maxCena}")]
        public async Task<IActionResult> GetPoMaxCeni(double maxCena)
        {
            var proizvodi = await _mediator.Send(new GetProizvodiPoMaxCeniQuery(maxCena));
            return Ok(proizvodi);
        }

        [HttpGet("dimenzije/filter")]
        public async Task<IActionResult> GetPoDimenzijama(
                          [FromQuery] double? maxSirina,
                          [FromQuery] double? maxVisina,
                          [FromQuery] double? maxDubina)
        {
            var proizvodi = await _mediator.Send(
                new GetProizvodiPoDimenzijamaQuery(maxSirina, maxVisina, maxDubina));

            return Ok(proizvodi);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ProizvodCreateDto dto)
        {
            var proizvod = await _mediator.Send(new CreateProizvodCommand(dto));
            return Ok(proizvod);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProizvodUpdateDto dto)
        {
            var proizvod = await _mediator.Send(new UpdateProizvodCommand(id, dto));

            if (proizvod == null)
                return NotFound("Proizvod nije pronađen.");

            return Ok(proizvod);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var obrisan = await _mediator.Send(new DeleteProizvodCommand(id));

                if (!obrisan)
                    return NotFound("Proizvod nije pronađen.");

                return Ok("Proizvod uspešno obrisan.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}