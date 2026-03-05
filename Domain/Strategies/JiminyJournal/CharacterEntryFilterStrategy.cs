using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.JiminyJournal;
using WayfinderProject.Domain.Models.JiminyJournal;

namespace WayfinderProject.Domain.Strategies.JiminyJournal
{
    public class CharacterEntryFilterStrategy : IDataFilterStrategy<CharacterEntry>
    {
        public IEnumerable<CharacterEntry> Filter(IEnumerable<CharacterEntry> data, FilterCriteria criteria)
        {
            if (criteria is not CharacterEntryCriteria characterEntryCriteria || !characterEntryCriteria.IsActive)
            {
                return data;
            }

            return data.Where(characterEntry =>
                !Utilities.FilterFailed(characterEntryCriteria.CharacterEntries, [characterEntry.Name]) &&
                !Utilities.FilterFailed(characterEntryCriteria.Characters, characterEntry.Characters) &&
                !Utilities.FilterFailed(characterEntryCriteria.Worlds, characterEntry.Worlds) &&
                (characterEntryCriteria.Games.Count == 0 || characterEntryCriteria.Games.Contains(characterEntry.Game))
            );
        }

        public FilterState RefreshAvailableOptions(IEnumerable<CharacterEntry> allData, FilterState state)
        {
            var rules = new List<FilterRule<CharacterEntry>>
            {
                new() { Id = "Characters", Selector = s => s.Characters },
                new() { Id = "Games", Selector = s => [s.Game] },
                new() { Id = "Entries", Selector = s => [s.Name] },
                new() { Id = "Worlds", Selector = s => s.Worlds }
            };

            return Utilities.RefreshJiminyJournal(allData, state, rules);
        }
    }
}
