using System.Text.Json.Serialization;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Models.MemoryArchive
{
    public class InterviewWrapper<T> : BaseWrapper<T> 
    {
        [JsonPropertyName("Interviews")]
        public override Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }

    public class Interview<TScriptLine> : BaseData<TScriptLine>, IFilterable where TScriptLine : ScriptLine
    {
        public string Link { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string AdditionalLink { get; set; } = string.Empty;
        public List<string> GameNames { get; set; } = new();
        public List<string> Participants { get; set; } = new();
        public string Provider { get; set; } = string.Empty;
        public string Translator { get; set; } = string.Empty;

        public bool ContainsText(string term, IEnumerable<object> subLines)
        {
            var lines = subLines.Cast<TScriptLine>();

            return lines.Any(line =>
                line.Line.Contains(term, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}