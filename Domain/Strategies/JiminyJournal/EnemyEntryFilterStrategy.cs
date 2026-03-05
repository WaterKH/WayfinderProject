using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.JiminyJournal;
using WayfinderProject.Domain.Models.JiminyJournal;

namespace WayfinderProject.Domain.Strategies.JiminyJournal
{
    public class EnemyEntryFilterStrategy : IDataFilterStrategy<EnemyEntry>
    {
        public IEnumerable<EnemyEntry> Filter(IEnumerable<EnemyEntry> data, FilterCriteria criteria)
        {
            if (criteria is not EnemyEntryCriteria enemyEntryCriteria || !enemyEntryCriteria.IsActive)
            {
                return data;
            }

            return data.Where(enemyEntry =>
                !Utilities.FilterFailed(enemyEntryCriteria.EnemyEntries, [enemyEntry.Name]) &&
                !Utilities.FilterFailed(enemyEntryCriteria.Characters, enemyEntry.Characters) &&
                !Utilities.FilterFailed(enemyEntryCriteria.Worlds, enemyEntry.Worlds) &&
                (enemyEntryCriteria.Games.Count == 0 || enemyEntryCriteria.Games.Contains(enemyEntry.Game))
            );
        }

        public FilterState RefreshAvailableOptions(IEnumerable<EnemyEntry> allData, FilterState state)
        {
            var rules = new List<FilterRule<EnemyEntry>>
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
