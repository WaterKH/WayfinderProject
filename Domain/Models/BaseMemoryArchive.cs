using WayfinderProject.Domain.Models.Attributes;

namespace WayfinderProject.Domain.Models
{
    public class BaseWrapper<T>
    {
        // The key is the Game Name (e.g., "Kingdom Hearts")
        public virtual Dictionary<string, List<T>> WrappedMemoryArchiveData { get; set; } = new();
    }

    public class BaseMemoryArchiveData<TSubData> : BaseData<TSubData>
    {
        public string Link { get; set; } = string.Empty;
        [DisplayInTable(headerName: "Areas", iconPath: "areas_gray.png", order: 4, colorClass: "blue")]
        public List<string> Areas { get; set; } = new();
        [DisplayInTable(headerName: "Characters", iconPath: "characters_gray.png", order: 5, colorClass: "orange")]
        public List<string> Characters { get; set; } = new();
        [DisplayInTable(headerName: "Music", iconPath: "music_gray.png", order: 6, colorClass: "purple")]
        public List<string> Music { get; set; } = new();
        [DisplayInTable(headerName: "Worlds", iconPath: "worlds_gray.png", order: 3, colorClass: "red")]
        public List<string> Worlds { get; set; } = new();
    }
}
