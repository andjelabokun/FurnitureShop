using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Application.DTOs;
using SalonNamestaja.Application.Features.Kategorije.Commands;
using SalonNamestaja.Application.Features.Kategorije.Queries;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KategorijeController : ControllerBase
{
    private readonly IMediator _mediator;

    public KategorijeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var kategorije = await _mediator.Send(new GetAllKategorijeQuery());
        return Ok(kategorije);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var kategorija = await _mediator.Send(new GetKategorijaByIdQuery(id));

        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");

        return Ok(kategorija);
    }

    [HttpGet("{id}/podkategorije")]
    public async Task<IActionResult> GetByIdSaPodkategorijama(int id)
    {
        var kategorija = await _mediator.Send(new GetKategorijaSaPodkategorijamaQuery(id));

        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");

        return Ok(kategorija);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(KategorijaCreateDto dto)
    {
        var kategorija = await _mediator.Send(new CreateKategorijaCommand(dto));
        return Ok(kategorija);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, KategorijaUpdateDto dto)
    {
        var kategorija = await _mediator.Send(new UpdateKategorijaCommand(id, dto));

        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");

        return Ok(kategorija);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var obrisana = await _mediator.Send(new DeleteKategorijaCommand(id));

            if (!obrisana)
                return NotFound("Kategorija nije pronađena.");

            return Ok("Kategorija uspešno obrisana.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}