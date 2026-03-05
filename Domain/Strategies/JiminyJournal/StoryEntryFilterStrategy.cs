using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.JiminyJournal;
using WayfinderProject.Domain.Models.JiminyJournal;

namespace WayfinderProject.Domain.Strategies.JiminyJournal
{
    public class StoryEntryFilterStrategy : IDataFilterStrategy<StoryEntry>
    {
        public IEnumerable<StoryEntry> Filter(IEnumerable<StoryEntry> data, FilterCriteria criteria)
        {
            if (criteria is not StoryEntryCriteria storyEntryCriteria || !storyEntryCriteria.IsActive)
            {
                return data;
            }

            return data.Where(storyEntry =>
                !Utilities.FilterFailed(storyEntryCriteria.StoryEntries, [storyEntry.Name]) &&
                !Utilities.FilterFailed(storyEntryCriteria.Characters, storyEntry.Characters) &&
                !Utilities.FilterFailed(storyEntryCriteria.Worlds, storyEntry.Worlds) &&
                (storyEntryCriteria.Games.Count == 0 || storyEntryCriteria.Games.Contains(storyEntry.Game))
            );
        }

        public FilterState RefreshAvailableOptions(IEnumerable<StoryEntry> allData, FilterState state)
        {
            var rules = new List<FilterRule<StoryEntry>>
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
