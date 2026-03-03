namespace WayfinderProject.Domain.Models.Filters.MemoryArchive
{
    public class SceneCriteria : MemoryArchiveCriteria
    {
        public List<string> Scenes { get; set; } = new();

        public override bool IsActive => Scenes.Count > 0 || Areas.Count > 0 ||
                                         Characters.Count > 0 || Music.Count > 0 ||
                                         Worlds.Count > 0 || Games.Count > 0;
    }
}
