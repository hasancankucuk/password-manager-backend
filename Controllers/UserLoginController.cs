using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using password_manager_backend.Data;
using password_manager_backend.Models;

namespace password_manager_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly password_manager_backendContext _context;

        public UserLoginController(password_manager_backendContext context)
        {
            _context = context;
        }

        // GET: api/UserLogin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLoginModel>>> GetUserLoginModel()
        {
            return await _context.UserLoginModel.ToListAsync();
        }

        // GET: api/UserLogin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLoginModel>> GetUserLoginModel(int id)
        {
            var userLoginModel = await _context.UserLoginModel.FindAsync(id);

            if (userLoginModel == null)
            {
                return NotFound();
            }

            return userLoginModel;
        }

        // PUT: api/UserLogin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLoginModel(int id, UserLoginModel userLoginModel)
        {
            if (id != userLoginModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(userLoginModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserLogin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserLoginModel>> PostUserLoginModel(UserLoginModel userLoginModel)
        {
            _context.UserLoginModel.Add(userLoginModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLoginModel", new { id = userLoginModel.Id }, userLoginModel);
        }

        // DELETE: api/UserLogin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLoginModel(int id)
        {
            var userLoginModel = await _context.UserLoginModel.FindAsync(id);
            if (userLoginModel == null)
            {
                return NotFound();
            }

            _context.UserLoginModel.Remove(userLoginModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserLoginModelExists(int id)
        {
            return _context.UserLoginModel.Any(e => e.Id == id);
        }
    }
}
