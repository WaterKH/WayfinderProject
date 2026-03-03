namespace WayfinderProject.Domain.Models.Filters
{
    public class FilterState
    {
        public string SearchTerm { get; set; } = string.Empty;
        public Dictionary<string, List<string>> Selected { get; set; } = new();
        public Dictionary<string, List<string>> Available { get; set; } = new();
        public Dictionary<string, List<string>> Initial { get; set; } = new();
        
        public void ClearSelections(string? filterId = null)
        {
            if (filterId == null)
            {
                Selected.Clear();
            }
            else
            {
                Selected.Remove(filterId);
            }
        }
    }
}
