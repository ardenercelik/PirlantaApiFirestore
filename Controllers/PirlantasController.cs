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
using PirlantaApi.Helpers;

namespace PirlantaApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PirlantasController : ControllerBase
    {
        private readonly ILogger _logger;
        IPirlantaRepository _repository;
        IMagazaRepository _magazaRepository;
        public PirlantasController( ILogger<PirlantasController> logger, IPirlantaRepository repository, IMagazaRepository magazaRepository)
        {
            _repository = repository;
            _magazaRepository = magazaRepository;
            _logger = logger;
        }

  
        // GET: api/Pirlantas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pirlanta>> GetPirlantaById(string id)
        {
            var pirlanta = await _repository.GetPirlanta(id);
            
            if (pirlanta == null)
            {
                return new Pirlanta();
            }
            return Ok(pirlanta);
        }
        [HttpGet("")]
        public async Task<ActionResult<List<Pirlanta>>> GetPirlantaByMagazaId([FromQuery]string magazaId, int pageNumber = 1)
        {
            var pirlanta = await _repository.GetPirlantaUnderMagaza(magazaId, pageNumber);

            if (pirlanta == null)
            {
                return new List<Pirlanta>();
            }
            return Ok(pirlanta);
        }

        [HttpGet("query")]
        public async Task<ActionResult<IEnumerable<Pirlanta>>> GetPirlantaQuery(string id,string cut, string color, string clarity, string cert, string magazaId,double caratMin , double caratMax , string type, int pageNumber = 1)
        {

            var pirlanta = await _repository.GetPirlantaFromQuery(id, cut, color, clarity, cert, magazaId, caratMin, caratMax, type, pageNumber);

            if (pirlanta == null)
            {
                return new List<Pirlanta>();
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
                return BadRequest(NetHelper.CreateErrorMessage("Id's does not match."));
            }
            if (!_repository.PirlantaExists(id))
            {
                return NotFound(NetHelper.CreateErrorMessage("Could not find item."));
            }
            try
            {
                await _repository.PutPirlanta(pirlanta);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(NetHelper.CreateSuccessMesseage($"Updated pirlanta {id}."));
        }

        // POST: api/Pirlantas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pirlanta>> PostPirlanta(Pirlanta pirlanta)
        {
            bool magazaExists = await _magazaRepository.MagazaExists(pirlanta.MagazaId);
            if (!magazaExists)
            {
                return NotFound(NetHelper.CreateErrorMessage($"Magaza with uid {pirlanta.MagazaId} does not exist"));
            }
            if (!_repository.PirlantaExists(pirlanta.PirlantaId))
            {
                return BadRequest(NetHelper.CreateErrorMessage("Item already exists."));
            }
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
                return NotFound(NetHelper.CreateErrorMessage("Could not find item."));
            }
            await _repository.DeletePirlanta(pirlanta);
            return NoContent();
        }

        
    }
}
