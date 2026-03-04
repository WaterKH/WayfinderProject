using System.Text.Json.Serialization;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Models.MemoryArchive
{
    public class SceneWrapper<T> : BaseWrapper<T> 
    {
        [JsonPropertyName("Scenes")]
        public override Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }

    public class Scene<TScriptLine> : BaseMemoryArchiveData<TScriptLine>, IFilterable where TScriptLine : ScriptLine
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