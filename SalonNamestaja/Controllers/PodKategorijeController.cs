using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PodKategorijeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PodKategorijeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var podkategorije = _unitOfWork.PodKategorije.GetAll();
        return Ok(podkategorije);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var podkategorija = _unitOfWork.PodKategorije.GetById(id);

        if (podkategorija == null)
            return NotFound("Podkategorija nije pronađena.");

        return Ok(podkategorija);
    }

    [HttpPost]
    public IActionResult Create(PodkategorijaCreateDto dto)
    {
        var podkategorija = new PodKategorija
        {
            Naziv = dto.Naziv,
            KategorijaID = dto.KategorijaID
        };

        _unitOfWork.PodKategorije.Add(podkategorija);
        _unitOfWork.SaveChanges();

        return Ok(podkategorija);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, PodkategorijaUpdateDto dto)
    {
        var podkategorija = _unitOfWork.PodKategorije.GetById(id);

        if (podkategorija == null)
            return NotFound("Podkategorija nije pronađena.");

        podkategorija.Naziv = dto.Naziv;
        podkategorija.KategorijaID = dto.KategorijaId;

        _unitOfWork.PodKategorije.Update(podkategorija);
        _unitOfWork.SaveChanges();

        return Ok(podkategorija);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var podkategorija = _unitOfWork.PodKategorije.GetById(id);

        if (podkategorija == null)
            return NotFound("Podkategorija nije pronađena.");

        _unitOfWork.PodKategorije.Remove(podkategorija);
        _unitOfWork.SaveChanges();

        return Ok("Podkategorija uspešno obrisana.");
    }
}