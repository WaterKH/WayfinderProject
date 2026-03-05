namespace WayfinderProject.Domain.Interfaces
{
    public interface IFilterable
    {
        bool ContainsText(string term, IEnumerable<object> data);
    }
}
