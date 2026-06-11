using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProizvodiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var proizvodi = _unitOfWork.Proizvodi.GetAll();
            return Ok(proizvodi);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var proizvod = _unitOfWork.Proizvodi.GetById(id);

            if (proizvod == null)
                return NotFound("Proizvod nije pronađen.");

            return Ok(proizvod);
        }

        [HttpGet("boja/{bojaId}")]
        public IActionResult GetPoBoji(int bojaId)
        {
            var proizvodi = _unitOfWork.Proizvodi.GetSviBojom(bojaId);
            return Ok(proizvodi);
        }

        [HttpPost]
        public IActionResult Create(ProizvodCreateDto dto)
        {
            var proizvod = new Proizvod
            {
                Naziv = dto.Naziv,
                Opis = dto.Opis,
                Cena = dto.Cena,
                StanjeNaLageru = dto.StanjeNaLageru,
                PodkategorijaID = dto.PodkategorijaId,
                MaterijalID = dto.MaterijalId,
                BojaID = dto.BojaId,
                DimenzijeID = dto.DimenzijeId,
                ProizvodjacID = dto.ProizvodjacId,
                SlikaUrl = dto.SlikaUrl
            };

            _unitOfWork.Proizvodi.Add(proizvod);
            _unitOfWork.SaveChanges();

            return Ok(proizvod);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProizvodUpdateDto dto)
        {
            var proizvod = _unitOfWork.Proizvodi.GetById(id);

            if (proizvod == null)
                return NotFound("Proizvod nije pronađen.");

            proizvod.Naziv = dto.Naziv;
            proizvod.Opis = dto.Opis;
            proizvod.Cena = dto.Cena;
            proizvod.StanjeNaLageru = dto.StanjeNaLageru;
            proizvod.PodkategorijaID = dto.PodkategorijaId;
            proizvod.MaterijalID = dto.MaterijalId;
            proizvod.BojaID = dto.BojaId;
            proizvod.DimenzijeID = dto.DimenzijeId;
            proizvod.SlikaUrl = dto.SlikaUrl;

            _unitOfWork.Proizvodi.Update(proizvod);
            _unitOfWork.SaveChanges();

            return Ok(proizvod);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var proizvod = _unitOfWork.Proizvodi.GetById(id);

            if (proizvod == null)
                return NotFound("Proizvod nije pronađen.");

            _unitOfWork.Proizvodi.Remove(proizvod);
            _unitOfWork.SaveChanges();

            return Ok("Proizvod uspešno obrisan.");
        }
    }
}