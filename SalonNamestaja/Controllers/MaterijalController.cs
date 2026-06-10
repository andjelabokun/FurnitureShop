using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaterijalController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MaterijalController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var materijali = _unitOfWork.Materijali.GetAll();
        return Ok(materijali);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var materijal = _unitOfWork.Materijali.GetById(id);

        if (materijal == null)
            return NotFound("Materijal nije pronađen.");

        return Ok(materijal);
    }

    [HttpPost]
    public IActionResult Create(MaterijalDto dto)
    {
        var materijal = new Materijal
        {
            Naziv = dto.Naziv,
            Tip = dto.Tip
        };

        _unitOfWork.Materijali.Add(materijal);
        _unitOfWork.SaveChanges();

        return Ok(materijal);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, MaterijalDto dto)
    {
        var materijal = _unitOfWork.Materijali.GetById(id);

        if (materijal == null)
            return NotFound("Materijal nije pronađen.");

        materijal.Naziv = dto.Naziv;
        materijal.Tip = dto.Tip;

        _unitOfWork.Materijali.Update(materijal);
        _unitOfWork.SaveChanges();

        return Ok(materijal);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var materijal = _unitOfWork.Materijali.GetById(id);

        if (materijal == null)
            return NotFound("Materijal nije pronađen.");

        _unitOfWork.Materijali.Remove(materijal);
        _unitOfWork.SaveChanges();

        return Ok("Materijal uspešno obrisan.");
    }
}