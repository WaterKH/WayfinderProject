using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.JiminyJournal;
using WayfinderProject.Domain.Models.JiminyJournal;

namespace WayfinderProject.Services.JiminyJournal
{
    public class EnemyEntryService :
        BaseDataService<
            EnemyEntry,
            EnemyEntryCriteria,
            EnemyEntryWrapper<EnemyEntry>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Entries", "Areas", "Characters", "Games", "Worlds"
        };

        public EnemyEntryService(HttpClient httpClient, IDataFilterStrategy<EnemyEntry> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<EnemyEntry> MapWrapperToData(EnemyEntryWrapper<EnemyEntry> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
