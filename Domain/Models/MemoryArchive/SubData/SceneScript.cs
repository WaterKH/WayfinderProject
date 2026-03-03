using System.Text.Json.Serialization;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Models.MemoryArchive
{
    public class SceneScriptWrapper : BaseSubWrapper<ScriptLine> 
    {
        [JsonPropertyName("Script")]
        public override Dictionary<string, List<ScriptLine>> WrappedMemoryArchiveSubData { get; set; } = new();
    }
}