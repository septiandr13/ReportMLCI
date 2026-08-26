using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportMLCI.Shared.Data;
using ReportMLCI.Shared.Models;

namespace ReportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterHeaderClauseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MasterHeaderClauseController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// GET: api/MasterHeaderClause
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MasterHeaderClause>>> GetMasterHeaderClauses()
        //{
        //    return await _context.MasterHeaderClauses.ToListAsync();
        //}

        // GET: api/MasterHeaderClause
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterHeaderClauseDto>>> GetMasterHeaderClauses()
        {
            var headersMap = new Dictionary<Guid, MasterHeaderClauseDto>();
            var clausesMap = new Dictionary<Guid, MasterClauseDto>();
            var detailsMap = new Dictionary<Guid, MasterClauseDetailDto>();

            // Query SQL murni Anda (Pastikan kolom ID dari setiap tabel di-select agar bisa di-mapping)
            string rawSql = @"
                            SELECT 
                                mhc.Id AS HeaderId,
                                mhc.ClauseHeaderCode,
                                mhc.ClauseHeaderTitle,
                                mhc.ClauseHeaderDescription,
                                mhc.IsActive AS IsActiveHeader,

                                mc.Id AS ClauseId,
                                mc.ClauseCode,
                                mc.ClauseTitle,
                                mc.ClauseContent,
                                mc.IsActive AS IsActiveClause,

                                mcd.Id AS DetailId,
                                mcd.ClauseSubCode,
                                mcd.ClauseSubTitle,
                                mcd.ClauseSubContent,
                                mcd.MasterClauseId,
                                mcd.IsActive AS IsActiveClauseSub,

                                mcsd.Id AS SubDetailId,
                                mcsd.SubDetailCode,
                                mcsd.SubDetailTitle,
                                mcsd.SubDetailContent,
                                mcsd.MasterClauseDetailId,
                                mcsd.IsActive AS IsActiveSubDetail
                            FROM MasterClauses mc
                            INNER JOIN MasterHeaderClauses mhc ON mhc.Id = mc.MasterHeaderClauseId
                            LEFT JOIN MasterClausesDetails mcd ON mc.Id = mcd.MasterClauseId
                            LEFT JOIN MasterClausesSubDetails mcsd ON mcsd.MasterClauseDetailId = mcd.Id
                            ORDER BY 
                                TRY_CAST(REPLACE(mc.ClauseCode, 'PASAL ', '') AS INT),
                                COALESCE(TRY_CAST(PARSENAME(mcd.ClauseSubCode, 2) AS INT), 999999) * 1000 + 
                                COALESCE(TRY_CAST(PARSENAME(mcd.ClauseSubCode, 1) AS INT), 999999),
                                CASE 
                                    WHEN mcsd.SubDetailCode = '(i)' THEN 1
                                    WHEN mcsd.SubDetailCode = '(ii)' THEN 2
                                    WHEN mcsd.SubDetailCode = '(iii)' THEN 3
                                    WHEN mcsd.SubDetailCode = '(iv)' THEN 4
                                    WHEN mcsd.SubDetailCode = '(v)' THEN 5
                                    WHEN mcsd.SubDetailCode = '(vi)' THEN 6
                                    WHEN mcsd.SubDetailCode = '(vii)' THEN 7
                                    WHEN mcsd.SubDetailCode = '(viii)' THEN 8
                                    WHEN mcsd.SubDetailCode = '(ix)' THEN 9
                                    WHEN mcsd.SubDetailCode = '(x)' THEN 10
                                    WHEN mcsd.SubDetailCode LIKE '[a-z])' THEN ASCII(LEFT(mcsd.SubDetailCode, 1)) + 100
                                    WHEN mcsd.SubDetailCode LIKE '[a-z].' THEN ASCII(LEFT(mcsd.SubDetailCode, 1)) + 200
                                    ELSE 999999
                                END";

            try
            {
                // Pinjam koneksi database yang digunakan oleh DbContext Anda
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = rawSql;
                    await _context.Database.OpenConnectionAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // 1. Pemetaan Level 0: Master Header Clause (Ganti ke GetGuid)
                            Guid headerId = reader.GetGuid(reader.GetOrdinal("HeaderId"));
                            if (!headersMap.TryGetValue(headerId, out var headerDto))
                            {
                                headerDto = new MasterHeaderClauseDto
                                {
                                    Id = headerId,
                                    ClauseHeaderCode = reader.IsDBNull(reader.GetOrdinal("ClauseHeaderCode")) ? null : reader.GetString(reader.GetOrdinal("ClauseHeaderCode")),
                                    ClauseHeaderTitle = reader.IsDBNull(reader.GetOrdinal("ClauseHeaderTitle")) ? null : reader.GetString(reader.GetOrdinal("ClauseHeaderTitle")),
                                    ClauseHeaderDescription = reader.IsDBNull(reader.GetOrdinal("ClauseHeaderDescription")) ? null : reader.GetString(reader.GetOrdinal("ClauseHeaderDescription")),
                                    IsActiveHeader = reader.GetBoolean(reader.GetOrdinal("IsActiveHeader"))
                                };
                                headersMap.Add(headerId, headerDto);
                            }

                            // 2. Pemetaan Level 1: Master Clause
                            if (!reader.IsDBNull(reader.GetOrdinal("ClauseId")))
                            {
                                Guid clauseId = reader.GetGuid(reader.GetOrdinal("ClauseId")); // Ganti ke GetGuid
                                if (!clausesMap.TryGetValue(clauseId, out var clauseDto))
                                {
                                    clauseDto = new MasterClauseDto
                                    {
                                        Id = clauseId,
                                        ClauseCode = reader.IsDBNull(reader.GetOrdinal("ClauseCode")) ? null : reader.GetString(reader.GetOrdinal("ClauseCode")),
                                        ClauseTitle = reader.IsDBNull(reader.GetOrdinal("ClauseTitle")) ? null : reader.GetString(reader.GetOrdinal("ClauseTitle")),
                                        ClauseContent = reader.IsDBNull(reader.GetOrdinal("ClauseContent")) ? null : reader.GetString(reader.GetOrdinal("ClauseContent")),
                                        IsActiveClause = reader.GetBoolean(reader.GetOrdinal("IsActiveClause"))
                                    };
                                    clausesMap.Add(clauseId, clauseDto);
                                    headerDto.MasterClauses.Add(clauseDto);
                                }

                                // 3. Pemetaan Level 2: Master Clause Detail
                                if (!reader.IsDBNull(reader.GetOrdinal("DetailId")))
                                {
                                    Guid detailId = reader.GetGuid(reader.GetOrdinal("DetailId")); // Ganti ke GetGuid
                                    if (!detailsMap.TryGetValue(detailId, out var detailDto))
                                    {
                                        detailDto = new MasterClauseDetailDto
                                        {
                                            Id = detailId,
                                            ClauseSubCode = reader.IsDBNull(reader.GetOrdinal("ClauseSubCode")) ? null : reader.GetString(reader.GetOrdinal("ClauseSubCode")),
                                            ClauseSubTitle = reader.IsDBNull(reader.GetOrdinal("ClauseSubTitle")) ? null : reader.GetString(reader.GetOrdinal("ClauseSubTitle")),
                                            ClauseSubContent = reader.IsDBNull(reader.GetOrdinal("ClauseSubContent")) ? null : reader.GetString(reader.GetOrdinal("ClauseSubContent")),
                                            IsActiveClauseSub = reader.GetBoolean(reader.GetOrdinal("IsActiveClauseSub"))
                                        };
                                        detailsMap.Add(detailId, detailDto);
                                        clauseDto.MasterClausesDetails.Add(detailDto);
                                    }

                                    // 4. Pemetaan Level 3: Master Clause Sub Detail
                                    if (!reader.IsDBNull(reader.GetOrdinal("SubDetailId")))
                                    {
                                        Guid subDetailId = reader.GetGuid(reader.GetOrdinal("SubDetailId")); // Ganti ke GetGuid
                                        var subDetailDto = new MasterClauseSubDetailDto
                                        {
                                            Id = subDetailId,
                                            SubDetailCode = reader.IsDBNull(reader.GetOrdinal("SubDetailCode")) ? null : reader.GetString(reader.GetOrdinal("SubDetailCode")),
                                            SubDetailTitle = reader.IsDBNull(reader.GetOrdinal("SubDetailTitle")) ? null : reader.GetString(reader.GetOrdinal("SubDetailTitle")),
                                            SubDetailContent = reader.IsDBNull(reader.GetOrdinal("SubDetailContent")) ? null : reader.GetString(reader.GetOrdinal("SubDetailContent")),
                                            IsActiveSubDetail = reader.GetBoolean(reader.GetOrdinal("IsActiveSubDetail"))
                                        };
                                        detailDto.MasterClausesSubDetails.Add(subDetailDto);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }

            // Mengembalikan hasil berupa list dari objek Header yang sudah ter-sort urut dari SQL Server
            return Ok(headersMap.Values.ToList());
        }

        // GET: api/MasterHeaderClause/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterHeaderClause>> GetMasterHeaderClause(Guid id)
        {
            var masterHeaderClause = await _context.MasterHeaderClauses.FindAsync(id);

            if (masterHeaderClause == null)
            {
                return NotFound();
            }

            return masterHeaderClause;
        }

        // PUT: api/MasterHeaderClause/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMasterHeaderClause(Guid id, MasterHeaderClause masterHeaderClause)
        {
            if (id != masterHeaderClause.Id)
            {
                return BadRequest();
            }

            _context.Entry(masterHeaderClause).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterHeaderClauseExists(id))
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

        // POST: api/MasterHeaderClause
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MasterHeaderClause>> PostMasterHeaderClause(MasterHeaderClause masterHeaderClause)
        {
            _context.MasterHeaderClauses.Add(masterHeaderClause);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMasterHeaderClause", new { id = masterHeaderClause.Id }, masterHeaderClause);
        }

        // DELETE: api/MasterHeaderClause/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterHeaderClause(Guid id)
        {
            var masterHeaderClause = await _context.MasterHeaderClauses.FindAsync(id);
            if (masterHeaderClause == null)
            {
                return NotFound();
            }

            _context.MasterHeaderClauses.Remove(masterHeaderClause);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MasterHeaderClauseExists(Guid id)
        {
            return _context.MasterHeaderClauses.Any(e => e.Id == id);
        }
    }
}
