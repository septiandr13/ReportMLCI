public class MasterHeaderClauseDto
{
    public Guid Id { get; set; } // Ubah dari int ke Guid
    public string ClauseHeaderCode { get; set; }
    public string ClauseHeaderTitle { get; set; }
    public string ClauseHeaderDescription { get; set; }
    public bool IsActiveHeader { get; set; }
    public List<MasterClauseDto> MasterClauses { get; set; } = new();
}

public class MasterClauseDto
{
    public Guid Id { get; set; } // Ubah dari int ke Guid
    public string ClauseCode { get; set; }
    public string ClauseTitle { get; set; }
    public string ClauseContent { get; set; }
    public bool IsActiveClause { get; set; }
    public List<MasterClauseDetailDto> MasterClausesDetails { get; set; } = new();
}

public class MasterClauseDetailDto
{
    public Guid Id { get; set; } // Ubah dari int ke Guid
    public string ClauseSubCode { get; set; }
    public string ClauseSubTitle { get; set; }
    public string ClauseSubContent { get; set; }
    public bool IsActiveClauseSub { get; set; }
    public List<MasterClauseSubDetailDto> MasterClausesSubDetails { get; set; } = new();
}

public class MasterClauseSubDetailDto
{
    public Guid Id { get; set; } // Ubah dari int ke Guid
    public string SubDetailCode { get; set; }
    public string SubDetailTitle { get; set; }
    public string SubDetailContent { get; set; }
    public bool IsActiveSubDetail { get; set; }
}