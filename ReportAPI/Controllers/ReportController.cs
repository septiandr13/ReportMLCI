using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportMLCI.Shared.Data;
using ReportMLCI.Shared.Models;

namespace ReportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/report/clause-mlci
        [HttpGet("clause-mlci")]
        public async Task<ActionResult<object>> GetClauseMLCIReport()
        {
            try
            {
                var headers = await _context.MasterHeaderClauses
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.ClauseHeaderCode)
                    .Include(h => h.MasterClauses)
                    .ThenInclude(c => c.MasterClausesDetails)
                    .ThenInclude(d => d.MasterClausesSubDetails)
                    .ToListAsync();

                var reportData = headers.Select(header => new
                {
                    header.Id,
                    header.ClauseHeaderCode,
                    header.ClauseHeaderTitle,
                    header.ClauseHeaderDescription,
                    header.IsActive,
                    clauses = header.MasterClauses
                              .Where(c => c.IsActive)
                              .OrderBy(c => c.ClauseCode)
                              .Select(clause => new
                    {
                        clause.Id,
                        clause.ClauseCode,
                        clause.ClauseTitle,
                        clause.ClauseContent,
                        clause.IsActive,
                        details = clause.MasterClausesDetails
                                  .Where(d => d.IsActive)
                                  .OrderBy(d => d.ClauseSubCode)
                                  .Select(detail => new
                        {
                            detail.Id,
                            detail.ClauseSubCode,
                            detail.ClauseSubTitle,
                            detail.ClauseSubContent,
                            detail.IsActive,
                            subDetails = detail.MasterClausesSubDetails
                                         .Where(s => s.IsActive)
                                         .OrderBy(s => s.SubDetailCode)
                                         .Select(subDetail => new
                            {
                                subDetail.Id,
                                subDetail.SubDetailCode,
                                subDetail.SubDetailTitle,
                                subDetail.SubDetailContent,
                                subDetail.IsActive
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList();

                return Ok(new
                {
                    success = true,
                    message = "Report data retrieved successfully",
                    data = reportData
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error retrieving report data",
                    error = ex.Message
                });
            }
        }

        // GET: api/report/clause-mlci/{id}
        [HttpGet("clause-mlci/{id}")]
        public async Task<ActionResult<object>> GetClauseMLCIReportById(Guid id)
        {
            try
            {
                var header = await _context.MasterHeaderClauses
                    .Where(h => h.Id == id && h.IsActive)
                    .OrderBy(h => h.ClauseHeaderCode)
                    .Include(h => h.MasterClauses)
                    .ThenInclude(c => c.MasterClausesDetails)
                    .ThenInclude(d => d.MasterClausesSubDetails)
                    .FirstOrDefaultAsync();

                if (header == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Report not found"
                    });
                }

                var reportData = new
                {
                    header.Id,
                    header.ClauseHeaderCode,
                    header.ClauseHeaderTitle,
                    header.ClauseHeaderDescription,
                    header.IsActive,
                    clauses = header.MasterClauses
                              .Where(c => c.IsActive)
                              .OrderBy(c => c.ClauseCode)
                              .Select(clause => new
                    {
                        clause.Id,
                        clause.ClauseCode,
                        clause.ClauseTitle,
                        clause.ClauseContent,
                        clause.IsActive,
                        details = clause.MasterClausesDetails
                                  .Where(d => d.IsActive)
                                  .OrderBy(d => d.ClauseSubCode)
                                  .Select(detail => new
                        {
                            detail.Id,
                            detail.ClauseSubCode,
                            detail.ClauseSubTitle,
                            detail.ClauseSubContent,
                            detail.IsActive,
                            subDetails = detail.MasterClausesSubDetails
                                        .Where(s => s.IsActive)
                                        .OrderBy(s => s.SubDetailCode)
                                        .Select(subDetail => new
                            {
                                subDetail.Id,
                                subDetail.SubDetailCode,
                                subDetail.SubDetailTitle,
                                subDetail.SubDetailContent,
                                subDetail.IsActive
                            }).ToList()
                        }).ToList()
                    }).ToList()
                };

                return Ok(new
                {
                    success = true,
                    message = "Report data retrieved successfully",
                    data = reportData
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error retrieving report data",
                    error = ex.Message
                });
            }
        }
    }
}
