using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Strategies
{
    public class TrailerFilterStrategy : IDataFilterStrategy<Trailer<ScriptLine>, TrailerScriptWrapper>
    {
        public IEnumerable<Trailer<ScriptLine>> Filter(IEnumerable<Trailer<ScriptLine>> data, FilterCriteria criteria)
        {
            if (criteria is not TrailerCriteria trailerCriteria || !trailerCriteria.IsActive)
            {
                return data;
            }

            return data.Where(trailer =>
                !Utilities.FilterFailed(trailerCriteria.Trailers, [trailer.Name]) &&
                !Utilities.FilterFailed(trailerCriteria.Areas, trailer.Areas) &&
                !Utilities.FilterFailed(trailerCriteria.Characters, trailer.Characters) &&
                !Utilities.FilterFailed(trailerCriteria.Music, trailer.Music) &&
                !Utilities.FilterFailed(trailerCriteria.Worlds, trailer.Worlds) &&
                (trailerCriteria.Games.Count == 0 || trailerCriteria.Games.Contains(trailer.Game))
            );
        }

        public FilterState RefreshAvailableOptions(
            IEnumerable<Trailer<ScriptLine>> allData, 
            IDictionary<string, TrailerScriptWrapper> subWrapper, 
            FilterState state)
        {
            var rules = new List<FilterRule<Trailer<ScriptLine>>>
            {
                new() { Id = "Areas", Selector = s => s.Areas },
                new() { Id = "Characters", Selector = s => s.Characters },
                new() { Id = "Games", Selector = s => [s.Game] },
                new() { Id = "Music", Selector = s => s.Music },
                new() { Id = "Trailers", Selector = s => [s.Name] },
                new() { Id = "Worlds", Selector = s => s.Worlds }
            };

            return Utilities.Refresh<Trailer<ScriptLine>, ScriptLine, TrailerScriptWrapper>(allData, subWrapper, state, rules);
        }
    }
}
