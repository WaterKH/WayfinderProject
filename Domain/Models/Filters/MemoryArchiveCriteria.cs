namespace WayfinderProject.Domain.Models.Filters
{
    public class MemoryArchiveCriteria : FilterCriteria
    {
        public List<string> Areas { get; set; } = new();
        public List<string> Characters { get; set; } = new();
        public List<string> Music { get; set; } = new();
        public List<string> Worlds { get; set; } = new();

        public virtual bool IsActive => Areas.Count > 0 || Characters.Count > 0 ||
                           Music.Count > 0 || Worlds.Count > 0 || Games.Count > 0;
    }
}
