namespace WayfinderProject.Domain.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayInTableAttribute(string headerName, string iconPath = "", int order = 0, string colorClass = "") : Attribute
    {
        public string HeaderName { get; } = headerName;
        public string IconPath { get; } = iconPath;
        public int Order { get; set; } = order;
        public string ColorClass { get; set; } = colorClass;
    }
}
