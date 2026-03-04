using System.Text.Json.Serialization;

namespace WayfinderProject.Domain.Models.JiminyJournal
{
    public class CharacterEntryWrapper<T> : BaseWrapper<T>
    {
        [JsonPropertyName("Characters")]
        public override Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }

    public class CharacterEntry : BaseJiminyJournalData
    { }
}
