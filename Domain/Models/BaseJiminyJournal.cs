using WayfinderProject.Domain.Models.Attributes;

namespace WayfinderProject.Domain.Models
{
    public class BaseJiminyJournalData : BaseData
    {
        [DisplayInTable(headerName: "Characters", iconPath: "characters_gray.png", order: 5, colorClass: "orange")]
        public List<string> Characters { get; set; } = new();
        [DisplayInTable(headerName: "Worlds", iconPath: "worlds_gray.png", order: 3, colorClass: "red")]
        public List<string> Worlds { get; set; } = new();
        public string Description { get; set; } = string.Empty;
        public string AdditionalInformation { get; set; } = string.Empty;
    }
}
