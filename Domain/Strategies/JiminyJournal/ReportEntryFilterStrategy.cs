using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.JiminyJournal;
using WayfinderProject.Domain.Models.JiminyJournal;

namespace WayfinderProject.Domain.Strategies.JiminyJournal
{
    public class ReportEntryFilterStrategy : IDataFilterStrategy<ReportEntry>
    {
        public IEnumerable<ReportEntry> Filter(IEnumerable<ReportEntry> data, FilterCriteria criteria)
        {
            if (criteria is not ReportEntryCriteria reportEntryCriteria || !reportEntryCriteria.IsActive)
            {
                return data;
            }

            return data.Where(reportEntry =>
                !Utilities.FilterFailed(reportEntryCriteria.ReportEntries, [reportEntry.Name]) &&
                !Utilities.FilterFailed(reportEntryCriteria.Characters, reportEntry.Characters) &&
                !Utilities.FilterFailed(reportEntryCriteria.Worlds, reportEntry.Worlds) &&
                (reportEntryCriteria.Games.Count == 0 || reportEntryCriteria.Games.Contains(reportEntry.Game))
            );
        }

        public FilterState RefreshAvailableOptions(IEnumerable<ReportEntry> allData, FilterState state)
        {
            var rules = new List<FilterRule<ReportEntry>>
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
