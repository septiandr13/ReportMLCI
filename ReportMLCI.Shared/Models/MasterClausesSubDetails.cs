namespace ReportMLCI.Shared.Models
{
    public class MasterClausesSubDetails
    {
        public Guid Id { get; set; }

        public Guid MasterClauseDetailId { get; set; }

        public string? SubDetailCode { get; set; }

        public string? SubDetailTitle { get; set; }

        public string? SubDetailContent { get; set; }

        public bool IsActive { get; set; } = true;
    }
}