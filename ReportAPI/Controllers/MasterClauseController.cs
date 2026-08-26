using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportMLCI.Shared.Data;
using ReportMLCI.Shared.Models;

namespace ReportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterClauseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MasterClauseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MasterClause
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterClause>>> GetMasterClauses()
        {
            return await _context.MasterClauses.ToListAsync();
        }

        // GET: api/MasterClause/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterClause>> GetMasterClause(Guid id)
        {
            var masterClause = await _context.MasterClauses.FindAsync(id);

            if (masterClause == null)
            {
                return NotFound();
            }

            return masterClause;
        }

        // PUT: api/MasterClause/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMasterClause(Guid id, MasterClause masterClause)
        {
            if (id != masterClause.Id)
            {
                return BadRequest();
            }

            _context.Entry(masterClause).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterClauseExists(id))
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

        // POST: api/MasterClause
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MasterClause>> PostMasterClause(MasterClause masterClause)
        {
            _context.MasterClauses.Add(masterClause);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMasterClause", new { id = masterClause.Id }, masterClause);
        }

        // DELETE: api/MasterClause/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterClause(Guid id)
        {
            var masterClause = await _context.MasterClauses.FindAsync(id);
            if (masterClause == null)
            {
                return NotFound();
            }

            _context.MasterClauses.Remove(masterClause);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MasterClauseExists(Guid id)
        {
            return _context.MasterClauses.Any(e => e.Id == id);
        }
    }
}
