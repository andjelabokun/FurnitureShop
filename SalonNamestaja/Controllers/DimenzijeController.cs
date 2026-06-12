using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestajaAPI.DTOs;
using SalonNamestajaAPI.Features.Dimenzije.Commands;
using SalonNamestajaAPI.Features.Dimenzije.Queries;

namespace SalonNamestajaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DimenzijeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DimenzijeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dimenzije = await _mediator.Send(new GetAllDimenzijeQuery());
            return Ok(dimenzije);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dimenzija = await _mediator.Send(new GetDimenzijeByIdQuery(id));

            if (dimenzija == null)
                return NotFound("Dimenzija nije pronađena.");

            return Ok(dimenzija);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(DimenzijeDto dto)
        {
            var dimenzija = await _mediator.Send(new CreateDimenzijeCommand(dto));
            return Ok(dimenzija);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DimenzijeDto dto)
        {
            var dimenzija = await _mediator.Send(new UpdateDimenzijeCommand(id, dto));

            if (dimenzija == null)
                return NotFound("Dimenzija nije pronađena.");

            return Ok(dimenzija);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obrisana = await _mediator.Send(new DeleteDimenzijeCommand(id));

            if (!obrisana)
                return NotFound("Dimenzija nije pronađena.");

            return Ok("Dimenzija uspešno obrisana.");
        }
    }
}