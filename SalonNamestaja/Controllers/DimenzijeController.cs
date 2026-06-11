using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers
{
   
        
        [ApiController]
        [Route("api/[controller]")]
        public class DimenzijeController : ControllerBase
        {
            private readonly IUnitOfWork _uow;

            public DimenzijeController(IUnitOfWork uow)
            {
                _uow = uow;
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                var dimenzije = _uow.Dimenzije.GetAll();
                return Ok(dimenzije);
            }

            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var dimenzija = _uow.Dimenzije.GetById(id);
                if (dimenzija == null)
                    return NotFound("Dimenzija nije pronađena.");
                return Ok(dimenzija);
            }

            [HttpPost]
            public IActionResult Create(DimenzijeDto dto)
            {
                var dimenzija = new Dimenzije
                {
                    Sirina = dto.Sirina,
                    Visina = dto.Visina,
                    Dubina = dto.Dubina
                };
                _uow.Dimenzije.Add(dimenzija);
                _uow.SaveChanges();
                return Ok(dimenzija);
            }

            [HttpPut("{id}")]
            public IActionResult Update(int id, DimenzijeDto dto)
            {
                var dimenzija = _uow.Dimenzije.GetById(id);
                if (dimenzija == null)
                    return NotFound("Dimenzija nije pronađena.");
                dimenzija.Sirina = dto.Sirina;
                dimenzija.Visina = dto.Visina;
                dimenzija.Dubina = dto.Dubina;
                _uow.Dimenzije.Update(dimenzija);
                _uow.SaveChanges();
                return Ok(dimenzija);
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var dimenzija = _uow.Dimenzije.GetById(id);
                if (dimenzija == null)
                    return NotFound("Dimenzija nije pronađena.");
                _uow.Dimenzije.Remove(dimenzija);
                _uow.SaveChanges();
                return Ok("Dimenzija uspešno obrisana.");
            }
        }
    }
