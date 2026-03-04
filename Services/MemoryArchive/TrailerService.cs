using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Services.MemoryArchive
{
    public class TrailerService :
        BaseDataService<
            Trailer<ScriptLine>,
            TrailerCriteria,
            ScriptLine,
            TrailerScriptWrapper,
            TrailerWrapper<Trailer<ScriptLine>>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Trailers", "Areas", "Characters", "Games", "Music", "Worlds"
        };

        public TrailerService(HttpClient httpClient, IDataFilterStrategy<Trailer<ScriptLine>, TrailerScriptWrapper> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<Trailer<ScriptLine>> MapWrapperToData(TrailerWrapper<Trailer<ScriptLine>> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
