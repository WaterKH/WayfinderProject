namespace WayfinderProject.Domain.Models.Filters.MemoryArchive
{
    public class InterviewCriteria : FilterCriteria
    {
        public List<string> Interviews { get; set; } = new();
        //public List<string> GameNames { get; set; } = new();
        public List<string> Participants { get; set; } = new();
        public List<string> Providers { get; set; } = new();
        public List<string> Translators { get; set; } = new();

        public bool IsActive => Interviews.Count > 0 || Participants.Count > 0 || 
                                Providers.Count > 0 || Translators.Count > 0 || 
                                Games.Count > 0;
    }
}
