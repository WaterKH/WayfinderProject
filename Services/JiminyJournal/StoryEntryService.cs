using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.JiminyJournal;
using WayfinderProject.Domain.Models.JiminyJournal;

namespace WayfinderProject.Services.JiminyJournal
{
    public class StoryEntryService :
        BaseDataService<
            StoryEntry,
            StoryEntryCriteria,
            StoryEntryWrapper<StoryEntry>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Entry", "Areas", "Characters", "Games", "Worlds"
        };

        public StoryEntryService(HttpClient httpClient, IDataFilterStrategy<StoryEntry> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<StoryEntry> MapWrapperToData(StoryEntryWrapper<StoryEntry> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
