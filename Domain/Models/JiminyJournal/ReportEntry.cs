using System.Text.Json.Serialization;

namespace WayfinderProject.Domain.Models.JiminyJournal
{
    public class ReportEntryWrapper<T> : BaseWrapper<T>
    {
        [JsonPropertyName("Reports")]
        public override Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }

    public class ReportEntry : BaseJiminyJournalData
    { }
}
