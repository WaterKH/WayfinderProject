namespace WayfinderProject.Domain.Models.Filters
{
    public class FilterCriteria
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string DataName { get; set; } = string.Empty;
        public List<string> Games { get; set; } = new();
    }
}
