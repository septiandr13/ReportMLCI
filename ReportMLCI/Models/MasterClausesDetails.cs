namespace ReportMLCI.Models
{
    public class MasterClausesDetails
    {
        public Guid Id { get; set; }
        public Guid MasterClauseId { get; set; }
        public string? ClauseSubCode { get; set; }
        public string? ClauseSubTitle { get; set; }
        public string? ClauseSubContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
