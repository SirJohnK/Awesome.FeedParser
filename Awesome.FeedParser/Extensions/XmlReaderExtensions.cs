using Awesome.FeedParser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Extensions
{
    public static class XmlReaderExtensions
    {
        /// <summary>
        /// Used for easy access of all elements read by an XmlReader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<string> AllElements(this XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                    yield return reader.Name;
            }
        }

        public static async Task<Dictionary<string, string>> AllSubTreeElements(this XmlReader reader)
        {
            var rootName = reader.Name;
            var elements = new Dictionary<string, string>();
            var subTree = reader.ReadSubtree();
            foreach (var element in subTree.AllElements())
            {
                if (!rootName.Equals(element) && !subTree.IsEmptyElement)
                {
                    var localName = subTree.LocalName;
                    subTree.ReadStartElement(element);
                    elements.Add(localName, await subTree.ReadContentAsStringAsync());
                }
            }
            subTree.Close();
            return elements;
        }

        public static NodeInformation NodeInformation(this XmlReader reader)
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

        public static ParseError ParseError(this XmlReader reader, string parser, ParseErrorType errorType, ParseType parseType, string? parseValue = null, string? message = null)
        {
            //Return new ParseError with Node and Parse information
            return new ParseError(reader.NodeInformation(), parser, errorType, parseType, parseValue, message);
        }
    }
}