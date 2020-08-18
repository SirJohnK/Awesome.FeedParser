using System.Xml;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Container for Xml node information.
    /// </summary>
    public class NodeInformation
    {
        /// <summary>
        /// The qualified name of the node.
        /// </summary>
        public string? Name { get; internal set; }

        /// <summary>
        /// The namespace prefix associated with the node.
        /// </summary>
        public string? Prefix { get; internal set; }

        /// <summary>
        /// The name of the node with the prefix removed.
        /// </summary>
        public string? LocalName { get; internal set; }

        /// <summary>
        /// Specifies the type of the node.
        /// </summary>
        public XmlNodeType? Type { get; internal set; }

        /// <summary>
        /// The node value, based on type.
        /// </summary>
        public string? Value { get; internal set; }

        /// <summary>
        /// The namespace URI of the node; otherwise an empty string.
        /// </summary>
        public string? Namespace { get; internal set; }

        /// <summary>
        /// The line number or 0 if no line information is available.
        /// </summary>
        public int? LineNumber { get; internal set; }

        /// <summary>
        /// The line position or 0 if no line information is available.
        /// </summary>
        public int? LinePosition { get; internal set; }

        /// <summary>
        /// The depth of the node in the XML document.
        /// </summary>
        public int? Depth { get; internal set; }

        /// <summary>
        /// true if the node is an element (NodeType equals XmlNodeType.Element) that ends with />; otherwise, false.
        /// </summary>
        public bool? IsEmpty { get; internal set; }

        /// <summary>
        /// true if the current node has attributes; otherwise, false.
        /// </summary>
        public bool? HasAttributes { get; internal set; }

        /// <summary>
        /// Custom ToString to return, if present, the node name.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name ?? base.ToString();
    }
}