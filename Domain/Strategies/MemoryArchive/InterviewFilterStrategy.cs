using Microsoft.VisualBasic;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;

namespace WayfinderProject.Domain.Strategies.MemoryArchive
{
    public class InterviewFilterStrategy : IDataFilterStrategy<Interview<ScriptLine>, InterviewDialogueWrapper>
    {
        public IEnumerable<Interview<ScriptLine>> Filter(IEnumerable<Interview<ScriptLine>> data, FilterCriteria criteria)
        {
            if (criteria is not InterviewCriteria interviewCriteria || !interviewCriteria.IsActive)
            {
                return data;
            }

            return data.Where(interview =>
                !Utilities.FilterFailed(interviewCriteria.Interviews, [interview.Name]) &&
                !Utilities.FilterFailed(interviewCriteria.Participants, interview.Participants) &&
                !Utilities.FilterFailed(interviewCriteria.Providers, [interview.Provider]) &&
                !Utilities.FilterFailed(interviewCriteria.Translators, [interview.Translator]) &&
                !Utilities.FilterFailed(interviewCriteria.Games, [interview.Game]) &&
                (
                    string.IsNullOrEmpty(interviewCriteria.SearchTerm) ||
                    interview.SubData.Any(line => line.Line.Contains(interviewCriteria.SearchTerm, StringComparison.OrdinalIgnoreCase))
                )
            );
        }

        public FilterState RefreshAvailableOptions(
            IEnumerable<Interview<ScriptLine>> allData,
            IDictionary<string, InterviewDialogueWrapper> subData,
            FilterState state)
        {
            var rules = new List<FilterRule<Interview<ScriptLine>>>
            {
                new() { Id = "Translators", Selector = s => [s.Translator] },
                new() { Id = "Participants", Selector = s => s.Participants },
                new() { Id = "Games", Selector = s => [s.Game] },
                new() { Id = "Interviews", Selector = s => [s.Name] },
                new() { Id = "Providers", Selector = s => [s.Provider] }
            };

            return Utilities.Refresh<Interview<ScriptLine>, ScriptLine, InterviewDialogueWrapper>(allData, subData, state, rules);
        }
    }
}
