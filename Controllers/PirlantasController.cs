using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PirlantaApi.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using PirlantaApi.Repository;

namespace PirlantaApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PirlantasController : ControllerBase
    {
        private readonly ILogger _logger;
        IPirlantaRepository _repository;
        public PirlantasController( ILogger<PirlantasController> logger, IPirlantaRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

  
        // GET: api/Pirlantas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pirlanta>> GetPirlantaById(string id)
        {
            var pirlanta = await _repository.GetPirlanta(id);
            
            if (pirlanta == null)
            {
                return NotFound();
            }
            return Ok(pirlanta);
        }
        [HttpGet("")]
        public async Task<ActionResult<List<Pirlanta>>> GetPirlantaByMagazaId([FromQuery]string magazaId, int pageNumber = 1)
        {
            var pirlanta = await _repository.GetPirlantaUnderMagaza(magazaId, pageNumber);

            if (pirlanta == null)
            {
                return NotFound();
            }
            return Ok(pirlanta);
        }

        [HttpGet("query")]
        public async Task<ActionResult<IEnumerable<Pirlanta>>> GetPirlantaQuery(string id,string cut, string color, string clarity, string cert, string magazaId,double caratMin , double caratMax , string type, int pageNumber = 1)
        {

            var pirlanta = await _repository.GetPirlantaFromQuery(id, cut, color, clarity, cert, magazaId, caratMin, caratMax, type, pageNumber);

            if (pirlanta == null)
            {
                return NotFound();
            }

            return Ok(pirlanta);
        }
        
        // PUT: api/Pirlantas/5
        //Check ekle
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPirlanta(string id, Pirlanta pirlanta)
        {
            if (id != pirlanta.PirlantaId)
            {
                return BadRequest();
            }
            if (!PirlantaExists(id))
            {
                return NotFound();
            }
            try
            {
                await _repository.PutPirlanta(pirlanta);
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Pirlantas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pirlanta>> PostPirlanta(Pirlanta pirlanta)
        {
            if (!PirlantaExists(pirlanta.PirlantaId))
            {
                return BadRequest();
            }
            //user id kontrol et
            await _repository.PostPirlanta(pirlanta);
            return StatusCode(201);
        }

        // DELETE: api/Pirlantas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePirlanta(string id)
        {
            var pirlanta = await _repository.GetPirlanta(id);
            if (pirlanta == null)
            {
                return NotFound();
            }

            await _repository.DeletePirlanta(pirlanta);

            return NoContent();
        }

        private bool PirlantaExists(string id)
        {
            if (_repository.GetPirlanta(id) != null) return true;
            return false;
        }
    }
}
