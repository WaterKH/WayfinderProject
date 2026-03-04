namespace WayfinderProject.Domain.Models.Filters.JiminyJournal
{
    public class ReportEntryCriteria : JiminyJournalCriteria
    {
        public List<string> ReportEntries { get; set; } = new();


        public override bool IsActive => ReportEntries.Count > 0 || Characters.Count > 0 || 
                                         Worlds.Count > 0 || Games.Count > 0;
    }
}
