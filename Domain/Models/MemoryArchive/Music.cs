using System.Text.Json.Serialization;

namespace WayfinderProject.Domain.Models.MemoryArchive
{
    public class MusicWrapper
    {
        [JsonPropertyName("Music")]
        public List<Music> Music { get; set; } = new();
    }

    public class Music
    {
        public string Name { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
