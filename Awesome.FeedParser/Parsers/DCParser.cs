using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Interfaces.Common;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Models.Common;
using Awesome.FeedParser.Models.DublinCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Dublin Core feed nodes.
    /// </summary>
    internal sealed class DCParser : BaseParser
    {
        /// <summary>
        /// Dublin Core Namespace URI:s.
        /// </summary>
        public static IEnumerable<string> Namespaces { get; } = new List<string>()
        {
            { @"http://purl.org/dc/elements/1.1/" },
            { @"http://purl.org/dc/terms/" },
            { @"http://purl.org/dc/dcmitype/" },
            { @"http://purl.org/dc/dcam/" },
        };

        /// <summary>
        /// Parser lazy loaded instance.
        /// </summary>
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new DCParser());

        /// <summary>
        /// Private constructor to prevent external initalization.
        /// </summary>
        private DCParser()
        {
        }

        /// <summary>
        /// Main Dublin Core parsing method.
        /// </summary>
        /// <param name="parent">Parent stack for current node.</param>
        /// <param name="reader">Current xml feed reader.</param>
        /// <param name="feed">Current feed result.</param>
        /// <param name="root">Flag indicating if parser is the default root parser.</param>
        /// <returns>Flag indicating if current node should be parsed or if next node should be retrieved.</returns>
        public override async Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true)
        {
            //Init
            bool result;

            //Verify Element Node
            if (result = reader.NodeType == XmlNodeType.Element && (!reader.IsEmptyElement || reader.HasAttributes))
            {
                //Init
                var nodeInfo = reader.NodeInformation();

                //Set target
                DCMetaData target;
                if (feed.CurrentItem != null)
                    target = feed.CurrentItem.DC ??= new DCMetaData();
                else
                    target = feed.DC ??= new DCMetaData();

                //Add Spotify to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.DublinCore;

                //Identify node name
                switch (reader.LocalName)
                {
                    case "publisher": //A person, an organization, or a service. Typically, the name of a Publisher should be used to indicate the entity.
                        {
                            //Get and Set feed/item title
                            var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);
                            target.Publisher = new FeedText() { Text = content };
                            break;
                        }

                    default: //Unknown feed/item node, continue to next.
                        {
                            result = false;
                            if (root) SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            break;
                        }
                }
            }

            //Return result
            return result;
        }
    }
}