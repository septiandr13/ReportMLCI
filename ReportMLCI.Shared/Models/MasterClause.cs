namespace ReportMLCI.Shared.Models
{
    public class MasterClause
    {
        public Guid Id { get; set; }
        public Guid MasterHeaderClauseId { get; set; }
        public string? ClauseCode { get; set; }
        public string? ClauseTitle { get; set; }
        public string? ClauseContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public MasterHeaderClause? MasterHeaderClause { get; set; }
        public List<MasterClausesDetails> MasterClausesDetails { get; set; } = new List<MasterClausesDetails>();
    }
}
