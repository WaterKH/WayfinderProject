namespace WayfinderProject.Domain.Models.Filters.JiminyJournal
{
    public class CharacterEntryCriteria : JiminyJournalCriteria
    {
        public List<string> CharacterEntries { get; set; } = new();


        public override bool IsActive => CharacterEntries.Count > 0 || Characters.Count > 0 || 
                                         Worlds.Count > 0 || Games.Count > 0;
    }
}
