using WayfinderProject.Domain.Interfaces;

namespace WayfinderProject.Domain.Models
{
    public class BaseSubWrapper<TSubData> : IBaseSubWrapper<TSubData>
    {
        public virtual Dictionary<string, List<TSubData>> WrappedSubData { get; set; } = new();
    }

    public class BaseSubData<TSubData>
    {
        public List<TSubData> SubData { get; set; } = new();
    }
}
