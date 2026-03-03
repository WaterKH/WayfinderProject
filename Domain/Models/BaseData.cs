using WayfinderProject.Domain.Models.Attributes;

namespace WayfinderProject.Domain.Models
{
    public class BaseData<TSubData> : BaseSubData<TSubData>
    {
        [DisplayInTable(headerName: "Name", iconPath: "scenes_gray.png", order: 2)]
        public string Name { get; set; } = string.Empty;
        [DisplayInTable(headerName: "Game", iconPath: "games_gray.png", order: 1)]
        public string Game { get; set; } = string.Empty;
    }
}
