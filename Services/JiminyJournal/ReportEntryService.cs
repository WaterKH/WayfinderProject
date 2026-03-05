using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.JiminyJournal;
using WayfinderProject.Domain.Models.JiminyJournal;

namespace WayfinderProject.Services.JiminyJournal
{
    public class ReportEntryService :
        BaseDataService<
            ReportEntry,
            ReportEntryCriteria,
            ReportEntryWrapper<ReportEntry>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Entries", "Areas", "Characters", "Games", "Worlds"
        };

        public ReportEntryService(HttpClient httpClient, IDataFilterStrategy<ReportEntry> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<ReportEntry> MapWrapperToData(ReportEntryWrapper<ReportEntry> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
