using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KupciController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public KupciController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var kupci = _unitOfWork.Kupci.GetAll();
        return Ok(kupci);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var kupac = _unitOfWork.Kupci.GetById(id);

        if (kupac == null)
            return NotFound("Kupac nije pronađen.");

        return Ok(kupac);
    }

    [HttpPost]
    public IActionResult Create(KupacCreateDto dto)
    {
        var kupac = new Kupac
        {
            Ime = dto.Ime,
            Prezime = dto.Prezime,
            Email = dto.Email,
            Telefon = dto.Telefon,
            TipKupca = dto.TipKupca,
            PIB = dto.PIB
        };

        _unitOfWork.Kupci.Add(kupac);
        _unitOfWork.SaveChanges();

        return Ok(kupac);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, KupacUpdateDto dto)
    {
        var kupac = _unitOfWork.Kupci.GetById(id);

        if (kupac == null)
            return NotFound("Kupac nije pronađen.");

        kupac.Ime = dto.Ime;
        kupac.Prezime = dto.Prezime;
        kupac.Email = dto.Email;
        kupac.Telefon = dto.Telefon;

        _unitOfWork.Kupci.Update(kupac);
        _unitOfWork.SaveChanges();

        return Ok(kupac);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var kupac = _unitOfWork.Kupci.GetById(id);

        if (kupac == null)
            return NotFound("Kupac nije pronađen.");

        _unitOfWork.Kupci.Remove(kupac);
        _unitOfWork.SaveChanges();

        return Ok("Kupac uspešno obrisan.");
    }
}