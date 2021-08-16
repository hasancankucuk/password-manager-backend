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
    public class UserInfoController : ControllerBase
    {
        private readonly password_manager_backendContext _context;

        public UserInfoController(password_manager_backendContext context)
        {
            _context = context;
        }

        // GET: api/UserInfoModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfoModel>>> GetUserInfoModel()
        {
            return await _context.UserInfoModel.ToListAsync();
        }

        // GET: api/UserInfoModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoModel>> GetUserInfoModel(int id)
        {
            var userInfoModel = await _context.UserInfoModel.FindAsync(id);

            if (userInfoModel == null)
            {
                return NotFound();
            }

            return userInfoModel;
        }

        // PUT: api/UserInfoModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInfoModel(int id, UserInfoModel userInfoModel)
        {
            if (id != userInfoModel.Id)
            {
                return BadRequest();
            }

            var user = await _context.UserInfoModel.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) {
                return BadRequest();
            }

            user.userEmail = userInfoModel.userEmail;
            user.userName = userInfoModel.userName;
            if (userInfoModel.userPassword.Trim().Length > 0) {
                user.userPassword = userInfoModel.userPassword; 
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/UserInfoModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("signup")]
        public async Task<ActionResult<UserInfoModel>> UserSignup(UserInfoModel userInfoModel)
        {
            var user = await _context.UserInfoModel.FirstOrDefaultAsync(x => x.userEmail == userInfoModel.userEmail);
            if (user != null)
            {
                return BadRequest();
            }

            _context.UserInfoModel.Add(userInfoModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserInfoModel", new { id = userInfoModel.Id }, userInfoModel);
        }

        [HttpPost("login")]
        public async Task<ActionResult> UserLogin(UserInfoModel userInfoModel)
        {
            var user = await _context.UserInfoModel.FirstOrDefaultAsync(x => (x.userEmail == userInfoModel.userEmail || x.userName == userInfoModel.userEmail) && x.userPassword == userInfoModel.userPassword);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/UserInfoModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInfoModel(int id)
        {
            var userInfoModel = await _context.UserInfoModel.FirstOrDefaultAsync(x => x.Id == id);
            if (userInfoModel == null)
            {
                return NotFound();
            }

            _context.UserInfoModel.Remove(userInfoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserInfoModelExists(int id)
        {
            return _context.UserInfoModel.Any(e => e.Id == id);
        }
    }
}
