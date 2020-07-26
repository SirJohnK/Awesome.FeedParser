using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Geo RSS Simple and GML feed nodes.
    /// </summary>
    internal sealed class GeoRSSParser : BaseParser
    {
        //Geo RSS Namespace URI:s
        public static string Namespace => @"http://www.georss.org/georss";

        public static string SecondNamespace => @"http://www.opengis.net/gml";

        //Parser lazy loaded instance
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new GeoRSSParser());

        //Private constructor to prevent external initalization
        private GeoRSSParser()
        {
        }

        /// <summary>
        /// Main Geo RSS parsing method.
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

                //Add Geo RSS to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.GeoRSS;

                //Identify node name
                switch (reader.LocalName)
                {
                    case "box":
                        {
                            break;
                        }

                    case "line":
                        {
                            break;
                        }

                    case "point":
                        {
                            if (!reader.IsEmptyElement)
                            {
                                //Init
                                var content = await reader.ReadStartElementAndContentAsStringAsync();
                                if (!string.IsNullOrWhiteSpace(content))
                                {
                                    var coords = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(part => part.Trim()).ToList();
                                }
                            }
                            break;
                        }

                    case "polygon":
                        {
                            break;
                        }

                    case "where":
                        {
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