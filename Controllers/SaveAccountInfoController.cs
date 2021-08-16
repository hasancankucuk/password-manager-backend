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
    public class SaveAccountInfoController : ControllerBase
    {
        private readonly password_manager_backendContext _context;

        public SaveAccountInfoController(password_manager_backendContext context)
        {
            _context = context;
        }

        // GET: api/SaveAccountInfo
        [HttpGet("allAccounts/{userId}")]
        [HttpGet("allAccounts/{userId}/{searchText}")]
        public async Task<ActionResult<IEnumerable<SaveAccountInfoModel>>> GetSaveAccountInfoModels(int userId, string searchText)
        {
            var accounts = _context.SaveAccountInfoModel.Where(x => x.UserInfoModelId == userId);
            if (!string.IsNullOrEmpty(searchText)) {
                accounts = accounts.Where(x => x.savedUrl.Contains(searchText) || x.savedUsername.Contains(searchText));
            }

            return await accounts.ToListAsync();
        }

        // GET: api/SaveAccountInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaveAccountInfoModel>> GetSaveAccountInfoModel(int id)
        {
            var saveAccountInfoModel = await _context.SaveAccountInfoModel.FindAsync(id);

            if (saveAccountInfoModel == null)
            {
                return NotFound();
            }

            return saveAccountInfoModel;
        }

        // PUT: api/SaveAccountInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaveAccountInfoModel(int id, [FromBody] SaveAccountInfoModel saveAccountInfoModel)
        {
            if (id != saveAccountInfoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(saveAccountInfoModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaveAccountInfoModelExists(id))
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

        // POST: api/SaveAccountInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaveAccountInfoModel>> PostSaveAccountInfoModel(SaveAccountInfoModel saveAccountInfoModel)
        {
            if (_context.SaveAccountInfoModel.Any(x => x.savedUsername == saveAccountInfoModel.savedUsername && x.savedUrl == saveAccountInfoModel.savedUrl))
            {
                return BadRequest();
            }

            _context.SaveAccountInfoModel.Add(saveAccountInfoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaveAccountInfoModel", new { id = saveAccountInfoModel.Id }, saveAccountInfoModel);
        }

        [HttpGet("recentlyUsedPasswords/{userId}")]
        public async Task<ActionResult> GetRecentlyUsedPassword(int userId)
        {
            var saveAccountInfoModels = _context.RecentlyUsedPasswords.Where(x => x.UserInfoModelId == userId).Select(x => x.SaveAccountInfoModel);
            return Ok(saveAccountInfoModels);
        }

        [HttpPost("recentlyUsedPassword/{accountId}")]
        public async Task<ActionResult> PostRecentlyUsedPassword(int accountId)
        {
            var saveAccountInfoModel = await _context.SaveAccountInfoModel.FirstOrDefaultAsync(x => x.Id == accountId);
            if (saveAccountInfoModel == null)
            {
                return BadRequest();
            }

            RecentlyUsedPassword recentlyUsedPassword = new RecentlyUsedPassword();
            recentlyUsedPassword.SaveAccountInfoModelId = saveAccountInfoModel.Id;
            recentlyUsedPassword.UserInfoModelId = saveAccountInfoModel.UserInfoModelId;
            var oldItem = _context.RecentlyUsedPasswords.Where(x => x.UserInfoModelId == saveAccountInfoModel.UserInfoModelId && x.SaveAccountInfoModelId == saveAccountInfoModel.Id);
            if (oldItem != null)
            {
                _context.RecentlyUsedPasswords.RemoveRange(oldItem);
            }

            var moreThanTen = _context.RecentlyUsedPasswords.Where(x => x.UserInfoModelId == saveAccountInfoModel.UserInfoModelId).Skip(10);
            if (moreThanTen != null)
            {
                _context.RecentlyUsedPasswords.RemoveRange(moreThanTen);
            }

            _context.RecentlyUsedPasswords.Add(recentlyUsedPassword);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/SaveAccountInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaveAccountInfoModel(int id)
        {
            var saveAccountInfoModel = await _context.SaveAccountInfoModel.FindAsync(id);
            if (saveAccountInfoModel == null)
            {
                return NotFound();
            }
            var recentlyUsedPassword = await _context.RecentlyUsedPasswords.Where(x => x.SaveAccountInfoModelId == saveAccountInfoModel.Id).ToListAsync();
            _context.RecentlyUsedPasswords.RemoveRange(recentlyUsedPassword);
            await _context.SaveChangesAsync();
            _context.SaveAccountInfoModel.Remove(saveAccountInfoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaveAccountInfoModelExists(int id)
        {
            return _context.SaveAccountInfoModel.Any(e => e.Id == id);
        }
    }
}
