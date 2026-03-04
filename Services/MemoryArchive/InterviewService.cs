using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Services.MemoryArchive
{
    public class InterviewService :
        BaseDataService<
            Interview<ScriptLine>,
            InterviewCriteria,
            ScriptLine,
            InterviewDialogueWrapper,
            InterviewWrapper<Interview<ScriptLine>>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Interviews", "Games", "Participants", "Providers", "Translators"
        };

        public InterviewService(HttpClient httpClient, IDataFilterStrategy<Interview<ScriptLine>, InterviewDialogueWrapper> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<Interview<ScriptLine>> MapWrapperToData(
            InterviewWrapper<Interview<ScriptLine>> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}