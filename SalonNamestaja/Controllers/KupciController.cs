using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestajaAPI.DTOs;
using SalonNamestajaAPI.Features.Kupci.Commands;
using SalonNamestajaAPI.Features.Kupci.Queries;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KupciController : ControllerBase
{
    private readonly IMediator _mediator;

    public KupciController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var kupci = await _mediator.Send(new GetAllKupciQuery());
        return Ok(kupci);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var kupac = await _mediator.Send(new GetKupacByIdQuery(id));

        if (kupac == null)
            return NotFound("Kupac nije pronađen.");

        return Ok(kupac);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(KupacCreateDto dto)
    {
        var kupac = await _mediator.Send(new CreateKupacCommand(dto));
        return Ok(kupac);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, KupacUpdateDto dto)
    {
        var kupac = await _mediator.Send(new UpdateKupacCommand(id, dto));

        if (kupac == null)
            return NotFound("Kupac nije pronađen.");

        return Ok(kupac);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var obrisan = await _mediator.Send(new DeleteKupacCommand(id));

        if (!obrisan)
            return NotFound("Kupac nije pronađen.");

        return Ok("Kupac uspešno obrisan.");
    }
}