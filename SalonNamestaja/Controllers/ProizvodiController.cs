using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestajaAPI.DTOs;
using SalonNamestajaAPI.Features.Proizvodi.Commands;
using SalonNamestajaAPI.Features.Proizvodi.Queries;

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