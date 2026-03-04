using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.JiminyJournal;
using WayfinderProject.Domain.Models.JiminyJournal;

namespace WayfinderProject.Services.JiminyJournal
{
    public class CharacterEntryService :
        BaseDataService<
            CharacterEntry,
            CharacterEntryCriteria,
            CharacterEntryWrapper<CharacterEntry>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Entry", "Areas", "Characters", "Games", "Worlds"
        };

        public CharacterEntryService(HttpClient httpClient, IDataFilterStrategy<CharacterEntry> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<CharacterEntry> MapWrapperToData(CharacterEntryWrapper<CharacterEntry> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
