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
    public class MasterClausesDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MasterClausesDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MasterClausesDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterClausesDetails>>> GetMasterClausesDetails()
        {
            return await _context.MasterClausesDetails.ToListAsync();
        }

        // GET: api/MasterClausesDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterClausesDetails>> GetMasterClausesDetails(Guid id)
        {
            var masterClausesDetails = await _context.MasterClausesDetails.FindAsync(id);

            if (masterClausesDetails == null)
            {
                return NotFound();
            }

            return masterClausesDetails;
        }

        // PUT: api/MasterClausesDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMasterClausesDetails(Guid id, MasterClausesDetails masterClausesDetails)
        {
            if (id != masterClausesDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(masterClausesDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterClausesDetailsExists(id))
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

        // POST: api/MasterClausesDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MasterClausesDetails>> PostMasterClausesDetails(MasterClausesDetails masterClausesDetails)
        {
            _context.MasterClausesDetails.Add(masterClausesDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMasterClausesDetails", new { id = masterClausesDetails.Id }, masterClausesDetails);
        }

        // DELETE: api/MasterClausesDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterClausesDetails(Guid id)
        {
            var masterClausesDetails = await _context.MasterClausesDetails.FindAsync(id);
            if (masterClausesDetails == null)
            {
                return NotFound();
            }

            _context.MasterClausesDetails.Remove(masterClausesDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MasterClausesDetailsExists(Guid id)
        {
            return _context.MasterClausesDetails.Any(e => e.Id == id);
        }
    }
}
