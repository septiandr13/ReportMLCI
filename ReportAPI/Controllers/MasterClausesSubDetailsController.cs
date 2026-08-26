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
    public class MasterClausesSubDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MasterClausesSubDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MasterClausesSubDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterClausesSubDetails>>> GetMasterClausesSubDetails()
        {
            return await _context.MasterClausesSubDetails.ToListAsync();
        }

        // GET: api/MasterClausesSubDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterClausesSubDetails>> GetMasterClausesSubDetails(Guid id)
        {
            var masterClausesSubDetails = await _context.MasterClausesSubDetails.FindAsync(id);

            if (masterClausesSubDetails == null)
            {
                return NotFound();
            }

            return masterClausesSubDetails;
        }

        // PUT: api/MasterClausesSubDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMasterClausesSubDetails(Guid id, MasterClausesSubDetails masterClausesSubDetails)
        {
            if (id != masterClausesSubDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(masterClausesSubDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterClausesSubDetailsExists(id))
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

        // POST: api/MasterClausesSubDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MasterClausesSubDetails>> PostMasterClausesSubDetails(MasterClausesSubDetails masterClausesSubDetails)
        {
            _context.MasterClausesSubDetails.Add(masterClausesSubDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMasterClausesSubDetails", new { id = masterClausesSubDetails.Id }, masterClausesSubDetails);
        }

        // DELETE: api/MasterClausesSubDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterClausesSubDetails(Guid id)
        {
            var masterClausesSubDetails = await _context.MasterClausesSubDetails.FindAsync(id);
            if (masterClausesSubDetails == null)
            {
                return NotFound();
            }

            _context.MasterClausesSubDetails.Remove(masterClausesSubDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MasterClausesSubDetailsExists(Guid id)
        {
            return _context.MasterClausesSubDetails.Any(e => e.Id == id);
        }
    }
}
