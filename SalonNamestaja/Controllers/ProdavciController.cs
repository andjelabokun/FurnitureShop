using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdavciController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProdavciController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var prodavci = _unitOfWork.Prodavci.GetAll();
        return Ok(prodavci);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var prodavac = _unitOfWork.Prodavci.GetById(id);

        if (prodavac == null)
            return NotFound("Prodavac nije pronađen.");

        return Ok(prodavac);
    }

    [HttpPost]
    public IActionResult Create(ProdavacCreateDto dto)
    {
        var prodavac = new Prodavac
        {
            Ime = dto.Ime,
            Prezime = dto.Prezime,
            KorisnickoIme = dto.KorisnickoIme,
            Lozinka = dto.Lozinka
        };

        _unitOfWork.Prodavci.Add(prodavac);
        _unitOfWork.SaveChanges();

        return Ok(prodavac);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, ProdavacUpdateDto dto)
    {
        var prodavac = _unitOfWork.Prodavci.GetById(id);

        if (prodavac == null)
            return NotFound("Prodavac nije pronađen.");

        prodavac.Ime = dto.Ime;
        prodavac.Prezime = dto.Prezime;
        prodavac.KorisnickoIme = dto.KorisnickoIme;

        _unitOfWork.Prodavci.Update(prodavac);
        _unitOfWork.SaveChanges();

        return Ok(prodavac);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var prodavac = _unitOfWork.Prodavci.GetById(id);

        if (prodavac == null)
            return NotFound("Prodavac nije pronađen.");

        _unitOfWork.Prodavci.Remove(prodavac);
        _unitOfWork.SaveChanges();

        return Ok("Prodavac uspešno obrisan.");
    }
}