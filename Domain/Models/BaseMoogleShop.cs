using WayfinderProject.Domain.Models.Attributes;

namespace WayfinderProject.Domain.Models
{
    public class BaseMoogleShopData<TSubData> : BaseData<TSubData>
    {
        [DisplayInTable(headerName: "Category", iconPath: "areas_gray.png", order: 3, colorClass: "blue")]
        public string Category { get; set; } = string.Empty;
    }
}
