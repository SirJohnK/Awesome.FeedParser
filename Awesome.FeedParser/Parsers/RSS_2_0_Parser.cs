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
    /// Parser for RSS 2.0 feed nodes.
    /// </summary>
    internal class RSS_2_0_Parser : RSS_1_0_Parser
    {
        //Parser lazy loaded instance
        public new static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RSS_2_0_Parser());

        //private constructor to prevent external initalization
        private RSS_2_0_Parser()
        {
        }

        /// <summary>
        /// Main RSS 2.0 parsing method.
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

                //Set common feed target
                ICommonFeed target = feed.CurrentItem ?? (ICommonFeed)feed;

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Optional

                    #region Feed

                    case "generator": //A string indicating the program used to generate the feed.
                        {
                            //Attempt to parse feed generator
                            feed.Generator ??= new FeedGenerator();
                            feed.Generator.Generator = await reader.ReadStartElementAndContentAsStringAsync();
                            break;
                        }

                    case "ttl": //Number of minutes that indicates how long a feed can be cached before refreshing from the source.
                        {
                            //Attempt to parse feed time to live
                            if (double.TryParse(await reader.ReadStartElementAndContentAsStringAsync(), out var ttl))
                                feed.Ttl = TimeSpan.FromMinutes(ttl);
                            break;
                        }

                    #endregion Feed

                    #region Item

                    case "author": //Email address of the author of the feed item.
                        {
                            if (feed.CurrentItem != null)
                            {
                                //Init
                                var content = await reader.ReadStartElementAndContentAsStringAsync();

                                try
                                {
                                    //Attempt to parse feed item author
                                    feed.CurrentItem.Author ??= new FeedPerson();
                                    feed.CurrentItem.Author.Email = content.ToMailAddress();
                                }
                                catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is FormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                                }
                            }
                            else
                                //Feed item object missing
                                throw new ArgumentNullException("Feed.CurrentItem");
                            break;
                        }

                    case "comments": //URL of a page for comments relating to the feed item.
                        {
                            if (feed.CurrentItem != null)
                            {
                                //Init
                                var content = await reader.ReadStartElementAndContentAsStringAsync();

                                try
                                {
                                    //Attempt to parse feed item comments URL
                                    feed.CurrentItem.Comments = new Uri(content);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                                }
                            }
                            else
                                //Feed item object missing
                                throw new ArgumentNullException("Feed.CurrentItem");
                            break;
                        }

                    case "guid": //A string/link that uniquely identifies the feed item.
                        {
                            if (feed.CurrentItem != null)
                            {
                                //Attempt to parse feed item guid
                                var isPermaLink = reader.GetAttribute("isPermaLink");
                                var content = await reader.ReadStartElementAndContentAsStringAsync();
                                feed.CurrentItem.Guid = new FeedGuid() { Guid = content };
                                if (bool.TryParse(isPermaLink, out var isLink))
                                {
                                    feed.CurrentItem.Guid.IsPermaLink = isLink;
                                    if (isLink)
                                    {
                                        try
                                        {
                                            //Attempt to parse feed item guid as URL
                                            feed.CurrentItem.Guid.Link = new Uri(content);
                                        }
                                        catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                        {
                                            //Unknown node format
                                            SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, $"Node: Link, {ex.Message}");
                                        }
                                    }
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
                            //Try RSS 1.0 Parse
                            result = await base.Parse(parent, reader, feed, false);
                            if (!result && root) SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            break;
                        }
                }
            }
            else
            {
                //Try RSS 1.0 Parse
                result = await base.Parse(parent, reader, feed, false);
            }

            //Return result
            return result;
        }
    }
}