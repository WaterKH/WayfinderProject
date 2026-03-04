namespace WayfinderProject.Domain.Models
{
    public class BaseWrapper<T>
    {
        // The key is the Game Name (e.g., "Kingdom Hearts")
        public virtual Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }
}
