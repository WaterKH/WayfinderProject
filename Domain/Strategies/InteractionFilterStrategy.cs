using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Strategies
{
    public class InteractionFilterStrategy : ISubDataFilterStrategy<Interaction<ScriptLine>, InteractionScriptWrapper>
    {
        public IEnumerable<Interaction<ScriptLine>> Filter(IEnumerable<Interaction<ScriptLine>> data, FilterCriteria criteria)
        {
            if (criteria is not InteractionCriteria interactionCriteria || !interactionCriteria.IsActive)
            {
                return data;
            }

            return data.Where(interaction =>
                !Utilities.FilterFailed(interactionCriteria.Interactions, [interaction.Name]) &&
                !Utilities.FilterFailed(interactionCriteria.Areas, interaction.Areas) &&
                !Utilities.FilterFailed(interactionCriteria.Characters, interaction.Characters) &&
                !Utilities.FilterFailed(interactionCriteria.Music, interaction.Music) &&
                !Utilities.FilterFailed(interactionCriteria.Worlds, interaction.Worlds) &&
                (interactionCriteria.Games.Count == 0 || interactionCriteria.Games.Contains(interaction.Game))
            );
        }

        public FilterState RefreshAvailableOptions(
            IEnumerable<Interaction<ScriptLine>> allData, 
            IDictionary<string, InteractionScriptWrapper> subData, 
            FilterState state)
        {
            var rules = new List<FilterRule<Interaction<ScriptLine>>>
            {
                new() { Id = "Areas", Selector = s => s.Areas },
                new() { Id = "Characters", Selector = s => s.Characters },
                new() { Id = "Games", Selector = s => [s.Game] },
                new() { Id = "Music", Selector = s => s.Music },
                new() { Id = "Interactions", Selector = s => [s.Name] },
                new() { Id = "Worlds", Selector = s => s.Worlds }
            };

            return Utilities.Refresh<Interaction<ScriptLine>, ScriptLine, InteractionScriptWrapper>(allData, subData, state, rules);
        }
    }
}
