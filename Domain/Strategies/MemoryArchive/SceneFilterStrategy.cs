using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Strategies.MemoryArchive
{
    public class SceneFilterStrategy : IDataFilterStrategy<Scene<ScriptLine>, SceneScriptWrapper>
    {
        public IEnumerable<Scene<ScriptLine>> Filter(IEnumerable<Scene<ScriptLine>> data, FilterCriteria criteria)
        {
            if (criteria is not SceneCriteria sceneCriteria || !sceneCriteria.IsActive)
            {
                return data;
            }

            return data.Where(scene =>
                !Utilities.FilterFailed(sceneCriteria.Scenes, [scene.Name]) &&
                !Utilities.FilterFailed(sceneCriteria.Areas, scene.Areas) &&
                !Utilities.FilterFailed(sceneCriteria.Characters, scene.Characters) &&
                !Utilities.FilterFailed(sceneCriteria.Music, scene.Music) &&
                !Utilities.FilterFailed(sceneCriteria.Worlds, scene.Worlds) &&
                (sceneCriteria.Games.Count == 0 || sceneCriteria.Games.Contains(scene.Game))
            );
        }

        public FilterState RefreshAvailableOptions(
            IEnumerable<Scene<ScriptLine>> allData,
            IDictionary<string, SceneScriptWrapper> subData,
            FilterState state)
        {
            var rules = new List<FilterRule<Scene<ScriptLine>>>
            {
                new() { Id = "Areas", Selector = s => s.Areas },
                new() { Id = "Characters", Selector = s => s.Characters },
                new() { Id = "Games", Selector = s => [s.Game] },
                new() { Id = "Music", Selector = s => s.Music },
                new() { Id = "Scenes", Selector = s => [s.Name] },
                new() { Id = "Worlds", Selector = s => s.Worlds }
            };

            return Utilities.Refresh<Scene<ScriptLine>, ScriptLine, SceneScriptWrapper>(allData, subData, state, rules);
        }
    }
}
