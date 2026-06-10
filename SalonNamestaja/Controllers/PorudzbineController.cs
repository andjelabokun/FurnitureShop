using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PorudzbineController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PorudzbineController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var porudzbine = _unitOfWork.Porudzbine.GetAll();
        return Ok(porudzbine);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var porudzbina = _unitOfWork.Porudzbine.GetById(id);

        if (porudzbina == null)
            return NotFound("Porudžbina nije pronađena.");

        return Ok(porudzbina);
    }

    [HttpPost]
    public IActionResult Create(PorudzbinaCreateDto dto)
    {
        var porudzbina = new Porudzbina
        {
            DatumVreme = DateTime.Now,
            Status = "Kreirana",
            UkupanIznos = dto.UkupanIznos,
            KupacID = dto.KupacID,
            ProdavacID = dto.ProdavacID
        };

        _unitOfWork.Porudzbine.Add(porudzbina);
        _unitOfWork.SaveChanges();

        return Ok(porudzbina);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, PorudzbinaUpdateDto dto)
    {
        var porudzbina = _unitOfWork.Porudzbine.GetById(id);

        if (porudzbina == null)
            return NotFound("Porudžbina nije pronađena.");

        porudzbina.Status = dto.Status;

        _unitOfWork.Porudzbine.Update(porudzbina);
        _unitOfWork.SaveChanges();

        return Ok(porudzbina);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var porudzbina = _unitOfWork.Porudzbine.GetById(id);

        if (porudzbina == null)
            return NotFound("Porudžbina nije pronađena.");

        _unitOfWork.Porudzbine.Remove(porudzbina);
        _unitOfWork.SaveChanges();

        return Ok("Porudžbina uspešno obrisana.");
    }
}