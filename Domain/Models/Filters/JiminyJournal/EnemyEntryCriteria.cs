namespace WayfinderProject.Domain.Models.Filters.JiminyJournal
{
    public class EnemyEntryCriteria : JiminyJournalCriteria
    {
        public List<string> EnemyEntries { get; set; } = new();


        public override bool IsActive => EnemyEntries.Count > 0 || Characters.Count > 0 || 
                                         Worlds.Count > 0 || Games.Count > 0;
    }
}
