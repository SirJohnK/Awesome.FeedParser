using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Models.Media;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for RSS 0.92 feed nodes.
    /// </summary>
    internal class RSS_0_92_Parser : RSS_0_91_Parser
    {
        //Parser lazy loaded instance
        public new static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RSS_0_92_Parser());

        //Protected constructor to prevent external initalization
        protected RSS_0_92_Parser()
        {
        }

        /// <summary>
        /// Main RSS 0.92 parsing method.
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

                //Set common feed target
                ICommonFeed target = feed.CurrentItem ?? (ICommonFeed)feed;

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Optional

                    #region Common

                    case "category": //One or more categories that the feed/item belongs to.
                        {
                            //Parse and add category to feed/item catergories list
                            var categories = target.Categories ?? new List<FeedCategory>();
                            categories.Add(new FeedCategory() { Domain = reader.GetAttribute("domain"), Category = await reader.ReadStartElementAndContentAsStringAsync() });
                            target.Categories = categories;
                            break;
                        }

                    #endregion Common

                    #region Feed

                    case "cloud": //Allows processes to register with a cloud to be notified of updates to the feed, implementing a lightweight publish-subscribe protocol for RSS feeds.
                        {
                            //Parse Cloud attributes
                            feed.Cloud = new FeedCloud()
                            {
                                Domain = reader.GetAttribute("domain"),
                                Path = reader.GetAttribute("path"),
                                Port = reader.GetAttribute("port"),
                                Protocol = reader.GetAttribute("protocol"),
                                RegisterProcedure = reader.GetAttribute("registerProcedure")
                            };
                            break;
                        }

                    #endregion Feed

                    #region Item

                    case "enclosure": //Media object that is attached to the feed item.
                        {
                            if (feed.CurrentItem != null)
                            {
                                //Attempt to parse enclosure
                                var content = new MediaContent() { Type = reader.GetAttribute("type") };

                                //Attempt to parse length
                                if (long.TryParse(reader.GetAttribute("length"), out var length))
                                    content.FileSize = length;

                                //Get enclosure url
                                var url = reader.GetAttribute("url");
                                try
                                {
                                    //Attempt to parse enclosure URL
                                    content.Url = new Uri(url);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, url, $"Node: url, {ex.Message}");
                                }

                                //Set item Enclosure
                                feed.CurrentItem.Enclosure = content;
                            }
                            else
                                //Feed item object missing
                                throw new ArgumentNullException("Feed.CurrentItem");
                            break;
                        }

                    case "source": //The feed that the feed item came from.
                        {
                            if (feed.CurrentItem != null)
                            {
                                //Get source url
                                var content = reader.GetAttribute("url");

                                //Attempt to parse source
                                feed.CurrentItem.Source = new FeedLink() { Type = FeedLinkType.Via, Text = await reader.ReadStartElementAndContentAsStringAsync() };

                                try
                                {
                                    //Attempt to parse enclosure URL
                                    feed.CurrentItem.Source.Url = new Uri(content);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, $"Node: url, {ex.Message}");
                                }
                            }
                            else
                                //Feed item object missing
                                throw new ArgumentNullException("Feed.CurrentItem");
                            break;
                        }

                    #endregion Item

                    #endregion Optional

                    default: //Unknown feed/item node
                        {
                            //Try RSS 0.91 Parse
                            result = await base.Parse(parent, reader, feed, false);
                            if (!result && root) SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            break;
                        }
                }
            }
            else
            {
                //Try RSS 0.91 Parse
                result = await base.Parse(parent, reader, feed, false);
            }

            //Return result
            return result;
        }
    }
}