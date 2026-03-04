namespace WayfinderProject.Domain.Models.Filters.JiminyJournal
{
    public class JiminyJournalCriteria : FilterCriteria
    {
        public List<string> Characters { get; set; } = new();
        public List<string> Worlds { get; set; } = new();

        public virtual bool IsActive => Characters.Count > 0 || Worlds.Count > 0 || Games.Count > 0;
    }
}
