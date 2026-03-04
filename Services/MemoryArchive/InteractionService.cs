using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Services.MemoryArchive
{
    public class InteractionService :
        SubBaseDataService<
            Interaction<ScriptLine>, 
            InteractionCriteria,
            ScriptLine,
            InteractionScriptWrapper,
            InteractionWrapper<Interaction<ScriptLine>>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Interactions", "Areas", "Characters", "Games", "Music", "Worlds"
        };

        public InteractionService(HttpClient httpClient, ISubDataFilterStrategy<Interaction<ScriptLine>, InteractionScriptWrapper> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<Interaction<ScriptLine>> MapWrapperToData(
            InteractionWrapper<Interaction<ScriptLine>> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
