using System.Text.Json.Serialization;

namespace WayfinderProject.Domain.Models.MoogleShop.SubData
{
    public class EnemyDropWrapper : BaseSubWrapper<EnemyDrop>
    {
        [JsonPropertyName("EnemyDrops")]
        public override Dictionary<string, List<EnemyDrop>> WrappedSubData { get; set; } = new();
    }

    public class EnemyDrop
    {
        public string EnemyName { get; set; } = string.Empty;
        public int DropRate { get; set; } = 0;
        public string AdditionalInformation { get; set; } = string.Empty;
    }
}