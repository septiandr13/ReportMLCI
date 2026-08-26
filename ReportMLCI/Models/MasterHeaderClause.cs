
namespace ReportMLCI.Models
{
    public class MasterHeaderClause
    {
        public Guid Id { get; set; }
        public string? ClauseHeaderCode { get; set; }
        public string? ClauseHeaderTitle { get; set; }
        public string? ClauseHeaderDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public List<MasterClause> MasterClauses { get; set; } = new List<MasterClause>();
    }
}
