using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _repository.GetUser(id);

            if (user == null)
            {
                return NotFound(NetHelper.CreateErrorMessage($"User does not exist."));
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Uid)
            {
                return BadRequest(NetHelper.CreateErrorMessage($"Id's does not match."));
            }
            bool magazaExists = await _repository.UserExists(id);

            if (!magazaExists)
            {
                return NotFound(NetHelper.CreateErrorMessage($"User does not exist."));
            }
            try
            {
               await _repository.PutUser(user);
            }
            catch (Exception)
            {
                    throw;
            }
            return Ok(NetHelper.CreateSuccessMesseage($"Updated user {id}."));
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user.Uid is null)
            {
                return BadRequest(NetHelper.CreateErrorMessage($"Did not provide user id."));
            }
            if (await _repository.UserExists(user.Uid))
            {
                return BadRequest(NetHelper.CreateErrorMessage($"User already exists"));
            }
            await _repository.PostUser(user);

            return StatusCode(201);
        }

        // DELETE: api/Users/5
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _repository.GetUser(id);
            if (user == null)
            {
                return NotFound(NetHelper.CreateErrorMessage($"User does not exist."));
            }
            await _repository.DeleteUser(user);
            return NoContent();
        }

        
    }
}
