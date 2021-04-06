using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PirlantaApi.Entities;
using PirlantaApi.Helpers;
using PirlantaApi.Repository;

namespace PirlantaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MagazasController : ControllerBase
    {
        IMagazaRepository _repository;
        public MagazasController( IMagazaRepository repository)
        {
            _repository = repository;
        }

        //// GET: api/magazas?pageNumber=2&pageSize=3
        //[HttpGet]
        //public async Task<ActionResult<Magaza>> GetMagazalar([FromQuery]int pageNumber)
        //{
        //    var response = await _repository.GetPagedMagazalar(pageNumber); 
        //    return Ok(response);
        //}

        // GET: api/Magazas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Magaza>> GetMagaza(string id)
        {
            var magaza = await _repository.GetMagaza(id);
            if (magaza == null)
            {
                return NotFound();
            }

            return magaza;
        }

        //[Authorize]
        [HttpGet("query")]
        public async Task<ActionResult<Magaza>> GetPirlantas(string uid)
        {
            var principal = HttpContext.User;
            string userId = GetClaim(principal, "user_id");
            Magaza magaza = new Magaza();

            //if (uid != userId)
            //{
            //    return Unauthorized();
            //}
            //else
            //{
                try
                {
                    magaza = await _repository.GetMagazaByUid(uid);
                }
                catch (Exception)
                {

                    return NotFound();
                }

                if(magaza is not null)
                {
                    magaza.Pirlantalar = magaza.Pirlantalar.OrderByDescending(c => c.DateUpdated).ToList();
                }
                return Ok(magaza);
            //}

        }

        // PUT: api/Magazas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagaza(string id, Magaza magaza)
        {
            if (id != magaza.MagazaId)
            {
                return BadRequest();
            }
            if (!MagazaExists(id))
            {
                return NotFound();
            }

            try
            {
                await _repository.PutMagaza(magaza);
            }
            catch (Exception)
            {
               
                throw;
            }

            return NoContent();
        }

        // POST: api/Magazas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Magaza>> PostMagaza(Magaza magaza)
        {
            await _repository.PostMagaza(magaza);

            return StatusCode(201);
        }

        // DELETE: api/Magazas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMagaza(string id)
        {
            var magaza = await _repository.FindMagaza(id);
            if (magaza == null)
            {
                return NotFound();
            }

            await _repository.DeleteMagaza(magaza);
            return NoContent();
        }
        private string GetClaim(ClaimsPrincipal principal, string key)
        {
            return principal?.Claims?.SingleOrDefault(p => p.Type == key)?.Value;
        }
        private bool MagazaExists(string id)
        {
            if (_repository.FindMagaza(id) != null) return true;
            return false;
        }

    }
}
