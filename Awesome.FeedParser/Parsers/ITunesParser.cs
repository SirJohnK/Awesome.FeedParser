using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for iTunes feed nodes.
    /// </summary>
    internal sealed class ITunesParser : BaseParser
    {
        //iTunes Namespace URI
        public static string Namespace { get; } = @"http://www.itunes.com/dtds/podcast-1.0.dtd";

        //Parser lazy loaded instance
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new ITunesParser());

        //Private constructor to prevent external initalization
        private ITunesParser()
        {
        }

        /// <summary>
        /// Main iTunes parsing method.
        /// </summary>
        /// <param name="reader">Current xml feed reader.</param>
        /// <param name="feed">Current feed result.</param>
        /// <returns></returns>
        public override async Task<bool> Parse(XmlReader reader, Feed feed)
        {
            //Init
            bool result;

            //Verify Element Node
            if (result = reader.NodeType == XmlNodeType.Element && (!reader.IsEmptyElement || reader.HasAttributes))
            {
                //Init
                ICommonITunes target;
                NodeInformation nodeInfo = reader.NodeInformation();

                //Add iTunes to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.ITunes;

                //Set common feed target
                if (feed.CurrentItem != null)
                {
                    feed.CurrentItem.ITunes ??= new ITunesItem();
                    target = feed.CurrentItem.ITunes;
                }
                else
                {
                    feed.ITunes ??= new ITunesFeed();
                    target = feed.ITunes;
                }

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Common

                    case "author": //The group responsible for creating the show/episode.
                        {
                            target.Author = await reader.ReadElementContentAsStringAsync();
                        }
                        break;

                    case "block": //The podcast/episode show or hide status. (true/false/yes/no)
                        {
                            var content = await reader.ReadElementContentAsStringAsync();
                            if (bool.TryParse(content, out var blockFlag))
                                target.Block = blockFlag;
                            else
                                target.Block = string.Equals(content, "yes");
                            break;
                        }

                    case "explicit": //The podcast/episode parental advisory information. Explicit language or adult content. (true/false/yes/no)
                        {
                            var content = await reader.ReadElementContentAsStringAsync();
                            if (bool.TryParse(content, out var explicitFlag))
                                target.Explicit = explicitFlag;
                            else
                                target.Explicit = string.Equals(content, "yes");
                            break;
                        }

                    case "image": //The artwork for the show/episode.
                        {
                            var href = reader.GetAttribute("href");
                            if (href != null) target.Image = new FeedImage() { Url = new Uri(href) };
                            result = false;
                            break;
                        }

                    case "keywords": //List of words or phrases used when searching.
                        {
                            var words = await reader.ReadElementContentAsStringAsync();
                            target.Keywords = words.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(word => word.Trim());
                            break;
                        }

                    case "title": //The show/episode title specific for Apple Podcasts.
                        {
                            target.Title = await reader.ReadElementContentAsStringAsync();
                            break;
                        }

                    case "subtitle": //Used as the title of the podcast/episode.
                        {
                            target.Subtitle = await reader.ReadElementContentAsStringAsync();
                            break;
                        }

                    case "summary": //Description of the podcast/episode.
                        {
                            target.Summary = await reader.ReadElementContentAsStringAsync();
                            break;
                        }

                    #endregion Common

                    #region Feed

                    case "type": //The type of show. Episodic (default) / Serial.
                        {
                            if (feed.ITunes != null)
                            {
                                var type = await reader.ReadElementContentAsStringAsync();
                                switch (type)
                                {
                                    case "episodic": feed.ITunes.Type = ITunesType.Episodic; break;
                                    case "serial": feed.ITunes.Type = ITunesType.Serial; break;
                                    default: SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, type); break;
                                }
                            }
                            else
                                result = false;
                            break;
                        }

                    case "category": //The show category information.
                        {
                            if (feed.ITunes != null)
                            {
                                //Get category text
                                var category = reader.GetAttribute("text");
                                if (!string.IsNullOrWhiteSpace(category))
                                {
                                    //Decode and save category
                                    var subCategories = new List<string>();
                                    feed.ITunes.Category ??= new Dictionary<string, IEnumerable<string>>();
                                    feed.ITunes.Category.Add(HttpUtility.HtmlDecode(category), subCategories);
                                    //Subcategories?
                                    if (!reader.IsEmptyElement)
                                    {
                                        var subTree = reader.ReadSubtree();
                                        while (subTree.ReadToFollowing("category", reader.NamespaceURI))
                                        {
                                            var subCategory = subTree.GetAttribute("text");
                                            if (!category.Equals(subCategory)) subCategories.Add(HttpUtility.HtmlDecode(subCategory));
                                        }
                                    }
                                }
                            }
                            result = false;
                            break;
                        }

                    case "owner": //The podcast owner contact information. Name and Email address.
                        {
                            if (feed.ITunes != null)
                            {
                                //Get owner information
                                var owner = new ITunesOwner();
                                var ownerElements = await reader.AllSubTreeElements();
                                foreach (var element in ownerElements)
                                {
                                    switch (element.Key)
                                    {
                                        case "name": owner.Name = element.Value; break;
                                        case "email": owner.Email = new MailAddress(element.Value); break;
                                        default: SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed, element.Value); break;
                                    }
                                }
                                feed.ITunes.Owner = owner;
                                await reader.SkipAsync();
                            }
                            else
                                result = false;
                            break;
                        }

                    case "new-feed-url": //The new podcast RSS Feed URL.
                        {
                            if (feed.ITunes != null)
                                feed.ITunes.NewFeedUrl = new Uri(await reader.ReadElementContentAsStringAsync());
                            else
                                result = false;
                            break;
                        }

                    #endregion Feed

                    #region Item

                    case "duration": //The duration of an episode.
                        {
                            if (feed.CurrentItem?.ITunes != null)
                            {
                                var content = await reader.ReadElementContentAsStringAsync();
                                if (double.TryParse(content, out var seconds))
                                    //If numeric, assume seconds
                                    feed.CurrentItem.ITunes.Duration = TimeSpan.FromSeconds(seconds);
                                else if (TimeSpan.TryParse(content, out var duration))
                                    //Duration in TimeSpan format
                                    feed.CurrentItem.ITunes.Duration = duration;
                                else
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            }
                            else
                                result = false;
                            break;
                        }

                    case "episodeType":
                        {
                            if (feed.CurrentItem?.ITunes != null)
                            {
                                var content = await reader.ReadElementContentAsStringAsync();
                                switch (content)
                                {
                                    case "full": feed.CurrentItem.ITunes.EpisodeType = ITunesEpisodeType.Full; break;
                                    case "trailer": feed.CurrentItem.ITunes.EpisodeType = ITunesEpisodeType.Trailer; break;
                                    case "bonus": feed.CurrentItem.ITunes.EpisodeType = ITunesEpisodeType.Bonus; break;
                                    default: SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content); break;
                                }
                            }
                            else
                                result = false;
                            break;
                        }

                    case "episode":
                        {
                            if (feed.CurrentItem?.ITunes != null)
                            {
                                var content = await reader.ReadElementContentAsStringAsync();
                                if (int.TryParse(content, out var episode))
                                    feed.CurrentItem.ITunes.Episode = episode;
                                else
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            }
                            else
                                result = false;
                            break;
                        }

                    case "season":
                        {
                            if (feed.CurrentItem?.ITunes != null)
                            {
                                var content = await reader.ReadElementContentAsStringAsync();
                                if (int.TryParse(content, out var season))
                                    feed.CurrentItem.ITunes.Season = season;
                                else
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            }
                            else
                                result = false;
                            break;
                        }

                    #endregion Item

                    default: //Unknown feed/item node, continue to next.
                        {
                            SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            result = false;
                            break;
                        }
                }
            }

            //Return result
            return result;
        }
    }
}