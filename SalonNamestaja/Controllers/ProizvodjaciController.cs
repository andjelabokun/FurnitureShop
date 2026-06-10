using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProizvodjaciController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProizvodjaciController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var proizvodjaci = _unitOfWork.Proizvodjaci.GetAll();
        return Ok(proizvodjaci);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var proizvodjac = _unitOfWork.Proizvodjaci.GetById(id);

        if (proizvodjac == null)
            return NotFound("Proizvođač nije pronađen.");

        return Ok(proizvodjac);
    }

    [HttpPost]
    public IActionResult Create(ProizvodjacDto dto)
    {
        var proizvodjac = new Proizvodjac
        {
            Naziv = dto.Naziv,
            Drzava = dto.Drzava
        };

        _unitOfWork.Proizvodjaci.Add(proizvodjac);
        _unitOfWork.SaveChanges();

        return Ok(proizvodjac);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, ProizvodjacDto dto)
    {
        var proizvodjac = _unitOfWork.Proizvodjaci.GetById(id);

        if (proizvodjac == null)
            return NotFound("Proizvođač nije pronađen.");

        proizvodjac.Naziv = dto.Naziv;
        proizvodjac.Drzava = dto.Drzava;

        _unitOfWork.Proizvodjaci.Update(proizvodjac);
        _unitOfWork.SaveChanges();

        return Ok(proizvodjac);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var proizvodjac = _unitOfWork.Proizvodjaci.GetById(id);

        if (proizvodjac == null)
            return NotFound("Proizvođač nije pronađen.");

        _unitOfWork.Proizvodjaci.Remove(proizvodjac);
        _unitOfWork.SaveChanges();

        return Ok("Proizvođač uspešno obrisan.");
    }
}