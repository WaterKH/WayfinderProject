using System.Text.Json.Serialization;

namespace WayfinderProject.Domain.Models.MoogleShop.SubData
{
    public class MaterialWrapper : BaseSubWrapper<Material>
    {
        [JsonPropertyName("MaterialsNeeded")]
        public override Dictionary<string, List<Material>> WrappedSubData { get; set; } = new();
    }

    public class Material
    {
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; } = 0;
    }
}