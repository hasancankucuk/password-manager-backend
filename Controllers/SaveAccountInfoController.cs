﻿using System;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaveAccountInfoModel>>> GetSaveAccountInfoModel()
        {
            return await _context.SaveAccountInfoModel.ToListAsync();
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
        public async Task<IActionResult> PutSaveAccountInfoModel(int id, SaveAccountInfoModel saveAccountInfoModel)
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
            if(_context.SaveAccountInfoModel.Any(x => x.savedUsername == saveAccountInfoModel.savedUsername && x.savedUrl == saveAccountInfoModel.savedUrl))
            {
                return BadRequest();
            }

            _context.SaveAccountInfoModel.Add(saveAccountInfoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaveAccountInfoModel", new { id = saveAccountInfoModel.Id }, saveAccountInfoModel);
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