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
        IUserRepository _userRepository;
        public MagazasController( IMagazaRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
    }


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

                //if(magaza is not null)
                //{
                //    magaza.Pirlantalar = magaza.Pirlantalar.OrderByDescending(c => c.DateUpdated).ToList();
                //}
                return Ok(magaza);
            //}

        }

        // PUT: api/Magazas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagaza(string id, Magaza magaza)
        {
            bool magazaExists = await _repository.MagazaExists(id);
            if (id != magaza.Uid)
            {
                return BadRequest(NetHelper.CreateErrorMessage($"Id's does not match."));
            }
            if (!magazaExists)
            {
                return NotFound(NetHelper.CreateErrorMessage($"Magaza does not exists"));
            }
            try
            {
                await _repository.PutMagaza(magaza);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(NetHelper.CreateSuccessMesseage($"Updated magaza {id}."));
        }

        // POST: api/Magazas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Magaza>> PostMagaza(Magaza magaza)
        {
            if (await _repository.MagazaExists(magaza.Uid))
            {
                return BadRequest(NetHelper.CreateErrorMessage($"Magaza already exists"));
            }
            bool magazaExists = await _userRepository.UserExists(magaza.Uid);
            if (!magazaExists)
            {
                return BadRequest(NetHelper.CreateErrorMessage($"Magaza does not have a user"));
            }

            await _repository.PostMagaza(magaza);

            return StatusCode(201);
        }

        // DELETE: api/Magazas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMagaza(string uid)
        {
            var magaza = await _repository.FindMagaza(uid);
            if (magaza == null)
            {
                return NotFound(NetHelper.CreateErrorMessage($"Magaza does not exists"));
            }
            await _repository.DeleteMagaza(magaza);
            return NoContent();
        }
        private string GetClaim(ClaimsPrincipal principal, string key)
        {
            return principal?.Claims?.SingleOrDefault(p => p.Type == key)?.Value;
        }

    }
}
