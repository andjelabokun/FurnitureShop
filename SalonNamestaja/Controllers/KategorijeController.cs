using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KategorijeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public KategorijeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var kategorije = _unitOfWork.Kategorije.GetAll();
        return Ok(kategorije);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var kategorija = _unitOfWork.Kategorije.GetById(id);

        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");

        return Ok(kategorija);
    }

    [HttpGet("{id}/podkategorije")]
    public IActionResult GetByIdSaPodkategorijama(int id)
    {
        var kategorija = _unitOfWork.Kategorije.GetByIdSaPodkategorijama(id);

        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");

        return Ok(kategorija);
    }

    [HttpPost]
    public IActionResult Create(KategorijaCreateDto dto)
    {
        var kategorija = new Kategorija

        {
            Naziv = dto.Naziv,
            SlikaUrl = dto.SlikaUrl
        };



        _unitOfWork.Kategorije.Add(kategorija);
        _unitOfWork.SaveChanges();

        return Ok(kategorija);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, KategorijaUpdateDto dto)
    {
        var kategorija = _unitOfWork.Kategorije.GetById(id);

        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");

        kategorija.Naziv = dto.Naziv;
        kategorija.SlikaUrl = dto.SlikaUrl;

        _unitOfWork.Kategorije.Update(kategorija);
        _unitOfWork.SaveChanges();

        return Ok(kategorija);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var kategorija = _unitOfWork.Kategorije.GetById(id);

        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");

        _unitOfWork.Kategorije.Remove(kategorija);
        _unitOfWork.SaveChanges();

        return Ok("Kategorija uspešno obrisana.");
    }
}