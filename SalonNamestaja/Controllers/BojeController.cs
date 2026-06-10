using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BojeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BojeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var boje = _unitOfWork.Boje.GetAll();
        return Ok(boje);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var boja = _unitOfWork.Boje.GetById(id);

        if (boja == null)
            return NotFound("Boja nije pronađena.");

        return Ok(boja);
    }

    [HttpPost]
    public IActionResult Create(BojaDto dto)
    {
        var boja = new Boja
        {
            Naziv = dto.Naziv
        };

        _unitOfWork.Boje.Add(boja);
        _unitOfWork.SaveChanges();

        return Ok(boja);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BojaDto dto)
    {
        var boja = _unitOfWork.Boje.GetById(id);

        if (boja == null)
            return NotFound("Boja nije pronađena.");

        boja.Naziv = dto.Naziv;

        _unitOfWork.Boje.Update(boja);
        _unitOfWork.SaveChanges();

        return Ok(boja);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var boja = _unitOfWork.Boje.GetById(id);

        if (boja == null)
            return NotFound("Boja nije pronađena.");

        _unitOfWork.Boje.Remove(boja);
        _unitOfWork.SaveChanges();

        return Ok("Boja uspešno obrisana.");
    }
}