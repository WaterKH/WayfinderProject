namespace WayfinderProject.Domain.Models.Filters.JiminyJournal
{
    public class StoryEntryCriteria : JiminyJournalCriteria
    {
        public List<string> StoryEntries { get; set; } = new();


        public override bool IsActive => StoryEntries.Count > 0 || Characters.Count > 0 || 
                                         Worlds.Count > 0 || Games.Count > 0 ||
                                         !string.IsNullOrEmpty(SearchTerm);
    }
}
