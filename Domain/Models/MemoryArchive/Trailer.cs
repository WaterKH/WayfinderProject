using System.Text.Json.Serialization;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Models.MemoryArchive
{
    public class TrailerWrapper<T> : BaseWrapper<T> 
    {
        [JsonPropertyName("Trailers")]
        public override Dictionary<string, List<T>> WrappedMemoryArchiveData { get; set; } = new();
    }

    public class Trailer<TScriptLine> : BaseMemoryArchiveData<TScriptLine>, IFilterable where TScriptLine : ScriptLine
    {
        public bool ContainsText(string term, IEnumerable<object> subLines)
        {
            var lines = subLines.Cast<TScriptLine>();

            return lines.Any(line =>
                line.Line.Contains(term, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}