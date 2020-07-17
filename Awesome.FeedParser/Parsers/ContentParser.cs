using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Content feed nodes.
    /// </summary>
    internal sealed class ContentParser : BaseParser
    {
        /// <summary>
        /// Content Namespace URI.
        /// </summary>
        public static string Namespace { get; } = @"http://purl.org/rss/1.0/modules/content/";

        /// <summary>
        /// Parser lazy loaded instance.
        /// </summary>
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new ContentParser());

        /// <summary>
        /// Private constructor to prevent external initalization.
        /// </summary>
        private ContentParser()
        {
        }

        /// <summary>
        /// Main Content parsing method.
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
                NodeInformation nodeInfo = reader.NodeInformation();

                //Add Content to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.Content;

                //Set common feed target
                ICommonContent target = feed.CurrentItem ?? (ICommonContent)feed;

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Common

                    case "encoded": //Contains the complete encoded content of the feed/item.
                        {
                            //Attemp to get encoded content
                            target.Content = new FeedContent() { Type = "text/html" };
                            target.Content.Text = await reader.ReadStartElementAndContentAsStringAsync(target.Content.Type);
                            break;
                        }

                    #endregion Common

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