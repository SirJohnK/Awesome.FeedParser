using Awesome.FeedParser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Awesome.FeedParser.Extensions
{
    /// <summary>
    /// Internal XmlReader extension methods.
    /// </summary>
    internal static class XmlReaderExtensions
    {
        /// <summary>
        /// Used for easy access of all elements read by an XmlReader.
        /// </summary>
        /// <param name="reader">Current XmlReader.</param>
        /// <returns>IEnumerable with all start element names.</returns>
        internal static IEnumerable<string> AllElements(this XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                    yield return reader.Name;
            }
        }

        /// <summary>
        /// Read all sub tree element names and values.
        /// </summary>
        /// <param name="reader">Current XmlReader.</param>
        /// <returns>List with KeyValuePair with element name and value.</returns>
        internal static async Task<List<KeyValuePair<string, string>>> AllSubTreeElements(this XmlReader reader)
        {
            var rootName = reader.Name;
            var elements = new List<KeyValuePair<string, string>>();
            var subTree = reader.ReadSubtree();
            foreach (var element in subTree.AllElements())
            {
                if (!rootName.Equals(element) && !subTree.IsEmptyElement)
                {
                    var localName = subTree.LocalName;
                    subTree.ReadStartElement(element);
                    var content = await subTree.ReadContentAsStringAsync().ConfigureAwait(false);
                    elements.Add(new KeyValuePair<string, string>(localName, content));
                }
            }
            subTree.Close();
            return elements;
        }

        /// <summary>
        /// Combines ReadStartElement and ReadContentAsStringAsync XmlReader methods.
        /// </summary>
        /// <param name="reader">Current XmlReader.</param>
        /// <param name="type">Text content type. (Mime type)</param>
        /// <returns></returns>
        internal static async Task<string> ReadStartElementAndContentAsStringAsync(this XmlReader reader, string? type = null)
        {
            //Init
            var text = string.Empty;

            //Read Start Element
            if (reader.NodeType == XmlNodeType.Element) reader.ReadStartElement();

            //Read node based on node type
            switch (reader.NodeType)
            {
                case XmlNodeType.CDATA:
                case XmlNodeType.EntityReference:
                case XmlNodeType.SignificantWhitespace:
                case XmlNodeType.Text:
                case XmlNodeType.Whitespace:
                    {
                        text = await reader.ReadContentAsStringAsync().ConfigureAwait(false);
                        break;
                    }

                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.DocumentType:
                case XmlNodeType.Element:
                case XmlNodeType.Entity:
                case XmlNodeType.Notation:
                case XmlNodeType.XmlDeclaration:
                    {
                        text = await reader.ReadOuterXmlAsync().ConfigureAwait(false);
                        break;
                    }
            }

            //Decode text based on text type
            switch (type)
            {
                case "html":
                case "text/html":
                    {
                        text = HttpUtility.HtmlDecode(text);
                        break;
                    }
            }

            //Return result
            return text;
        }

        /// <summary>
        /// Extract current node information.
        /// </summary>
        /// <param name="reader">Current XmlReader.</param>
        /// <returns>Complete NodeInformation for current node.</returns>
        internal static NodeInformation NodeInformation(this XmlReader reader)
        {
            //Init
            var lineInfo = (IXmlLineInfo)reader;

            //Return Node Information
            return new NodeInformation()
            {
                Name = reader.Name,
                Prefix = reader.Prefix,
                LocalName = reader.LocalName,
                Type = reader.NodeType,
                Value = reader.Value,
                Namespace = reader.NamespaceURI,
                LineNumber = lineInfo.LineNumber,
                LinePosition = lineInfo.LinePosition,
                Depth = reader.Depth,
                IsEmpty = reader.IsEmptyElement,
                HasAttributes = reader.HasAttributes,
            };
        }
    }
}