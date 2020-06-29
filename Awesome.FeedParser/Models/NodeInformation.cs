using System.Xml;

namespace Awesome.FeedParser.Models
{
    public class NodeInformation
    {
        public string? Name { get; internal set; }
        public XmlNodeType? Type { get; internal set; }
        public string? Value { get; internal set; }
        public string? Namespace { get; internal set; }
        public int? LineNumber { get; internal set; }
        public int? LinePosition { get; internal set; }
        public int? Depth { get; internal set; }
        public bool? IsEmpty { get; internal set; }
        public bool? HasAttributes { get; internal set; }
    }
}