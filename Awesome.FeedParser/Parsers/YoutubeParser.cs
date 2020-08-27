using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Youtube feed nodes.
    /// </summary>
    internal sealed class YoutubeParser : BaseParser
    {
        /// <summary>
        /// Youtube Namespace URI:s.
        /// </summary>
        public static IEnumerable<string> Namespaces { get; } = new List<string>()
        {
            { @"http://www.youtube.com/xml/schemas/2015" },
        };

        /// <summary>
        /// Parser lazy loaded instance.
        /// </summary>
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new YoutubeParser());

        /// <summary>
        /// Private constructor to prevent external initalization.
        /// </summary>
        private YoutubeParser()
        {
        }

        /// <summary>
        /// Main Youtube parsing method.
        /// </summary>
        /// <param name="parent">Parent stack for current node.</param>
        /// <param name="reader">Current xml feed reader.</param>
        /// <param name="feed">Current feed result.</param>
        /// <param name="root">Flag indicating if parser is the default root parser.</param>
        /// <returns>Flag indicating if current node should be parsed or if next node should be retrieved.</returns>
        public override Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true)
        {
            return Task.FromResult(false);
        }
    }
}