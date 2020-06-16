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
                    yield return reader.LocalName;
            }
        }

        public static async Task<Dictionary<string, string>> AllSubTreeElements(this XmlReader reader)
        {
            var rootName = reader.LocalName;
            var elements = new Dictionary<string, string>();
            var subTree = reader.ReadSubtree();
            foreach (var element in subTree.AllElements())
            {
                if (!rootName.Equals(element) && !subTree.IsEmptyElement)
                {
                    subTree.ReadStartElement(element);
                    elements.Add(element, await subTree.ReadContentAsStringAsync());
                }
            }
            subTree.Close();
            return elements;
        }
    }
}