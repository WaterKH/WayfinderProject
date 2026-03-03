namespace WayfinderProject.Domain.Models.Filters
{
    public class FilterMetadata
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#ffffff";
        public List<string> Initial { get; set; } = new();
        public List<string> Selected { get; set; } = new();
        public List<string> Available { get; set; } = new();
    }
}
