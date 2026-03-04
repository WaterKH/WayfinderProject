using System.Text.Json.Serialization;

namespace WayfinderProject.Domain.Models.JiminyJournal
{
    public class StoryEntryWrapper<T> : BaseWrapper<T>
    {
        [JsonPropertyName("Story")]
        public override Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }

    public class StoryEntry : BaseJiminyJournalData
    { }
}
