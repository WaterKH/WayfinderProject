namespace WayfinderProject.Domain.Interfaces
{
    public interface IBaseSubWrapper<TSubData>
    {
        Dictionary<string, List<TSubData>> WrappedMemoryArchiveSubData { get; set; }
    }
}
