using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Services.MemoryArchive
{
    public class SceneService :
        SubBaseDataService<
            Scene<ScriptLine>, 
            SceneCriteria,
            ScriptLine,
            SceneScriptWrapper,
            SceneWrapper<Scene<ScriptLine>>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Scenes", "Areas", "Characters", "Games", "Music", "Worlds"
        };

        public SceneService(HttpClient httpClient, ISubDataFilterStrategy<Scene<ScriptLine>, SceneScriptWrapper> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<Scene<ScriptLine>> MapWrapperToData(SceneWrapper<Scene<ScriptLine>> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
