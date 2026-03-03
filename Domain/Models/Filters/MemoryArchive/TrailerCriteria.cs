namespace WayfinderProject.Domain.Models.Filters.MemoryArchive
{
    public class TrailerCriteria : MemoryArchiveCriteria
    {
        public List<string> Trailers { get; set; } = new();

        public override bool IsActive => Trailers.Count > 0 || Areas.Count > 0 ||
                                         Characters.Count > 0 || Music.Count > 0 ||
                                         Worlds.Count > 0 || Games.Count > 0;
    }
}
