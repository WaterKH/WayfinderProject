namespace WayfinderProject.Domain.Models.Filters
{
    public class FilterRule<T>
    {
        public string Id { get; set; } = string.Empty;
        public Func<T, IEnumerable<string>> Selector { get; set; } = default!;
    }
}
