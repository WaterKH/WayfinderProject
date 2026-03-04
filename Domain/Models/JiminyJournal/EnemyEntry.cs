using System.Text.Json.Serialization;

namespace WayfinderProject.Domain.Models.JiminyJournal
{
    public class EnemyEntryWrapper<T> : BaseWrapper<T>
    {
        [JsonPropertyName("Enemies")]
        public override Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }

    public class EnemyEntry : BaseJiminyJournalData
    { }
}
