using System.Text.Json.Serialization;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Models.MemoryArchive
{
    public class InterviewDialogueWrapper : BaseSubWrapper<ScriptLine> 
    {
        [JsonPropertyName("Script")]
        public override Dictionary<string, List<ScriptLine>> WrappedSubData { get; set; } = new();
    }
}