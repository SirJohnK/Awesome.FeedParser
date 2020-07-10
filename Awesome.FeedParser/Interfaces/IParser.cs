using Awesome.FeedParser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Base Parser Interface.
    /// </summary>
    internal interface IParser
    {
        /// <summary>
        /// Main parser parse method.
        /// </summary>
        /// <param name="parent">Parent stack for current node.</param>
        /// <param name="reader">Current xml feed reader.</param>
        /// <param name="feed">Current feed result.</param>
        /// <param name="root">Flag indicating if parser is the default root parser.</param>
        /// <returns>Flag indicating if current node should be parsed or if next node should be retrieved.</returns>
        Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true);
    }
}