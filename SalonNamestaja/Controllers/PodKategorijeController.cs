using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestajaAPI.DTOs;
using SalonNamestajaAPI.Features.PodKategorije.Commands;
using SalonNamestajaAPI.Features.PodKategorije.Queries;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PodKategorijeController : ControllerBase
{
    private readonly IMediator _mediator;

    public PodKategorijeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var podkategorije = await _mediator.Send(new GetAllPodKategorijeQuery());
        return Ok(podkategorije);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var podkategorija = await _mediator.Send(new GetPodKategorijaByIdQuery(id));

        if (podkategorija == null)
            return NotFound("Podkategorija nije pronađena.");

        return Ok(podkategorija);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(PodkategorijaCreateDto dto)
    {
        var podkategorija = await _mediator.Send(new CreatePodKategorijaCommand(dto));
        return Ok(podkategorija);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PodkategorijaUpdateDto dto)
    {
        var podkategorija = await _mediator.Send(new UpdatePodKategorijaCommand(id, dto));

        if (podkategorija == null)
            return NotFound("Podkategorija nije pronađena.");

        return Ok(podkategorija);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var obrisana = await _mediator.Send(new DeletePodKategorijaCommand(id));

        if (!obrisana)
            return NotFound("Podkategorija nije pronađena.");

        return Ok("Podkategorija uspešno obrisana.");
    }
}