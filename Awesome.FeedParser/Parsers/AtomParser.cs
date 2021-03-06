﻿using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Interfaces.Atom;
using Awesome.FeedParser.Interfaces.Common;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Models.Atom;
using Awesome.FeedParser.Models.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Atom feed nodes.
    /// </summary>
    internal sealed class AtomParser : BaseParser
    {
        /// <summary>
        /// Atom Namespace URI:s.
        /// </summary>
        public static IEnumerable<string> Namespaces { get; } = new List<string>()
        {
            { @"http://www.w3.org/2005/Atom" },
        };

        /// <summary>
        /// Parser lazy loaded instance.
        /// </summary>
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new AtomParser());

        /// <summary>
        /// Private constructor to prevent external initalization.
        /// </summary>
        private AtomParser()
        {
        }

        /// <summary>
        /// Main Atom parsing method.
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
                ICommonAtom target = feed;
                ICommonAtomFeed? targetFeed = feed;
                ICommonAtomEntry? targetEntry = feed.CurrentItem;
                NodeInformation nodeInfo = reader.NodeInformation();

                //Verify feed type
                if (feed.Type == FeedType.Atom)
                {
                    //Set common target
                    if (feed.CurrentItem != null)
                        target = feed.CurrentItem;
                }
                else
                {
                    //Add Atom to feed content type
                    ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                    feedTarget.ContentType |= FeedContentType.Atom;

                    //Set common target
                    if (feed.CurrentItem != null)
                    {
                        feed.CurrentItem.Atom ??= new AtomEntry();
                        target = feed.CurrentItem.Atom;
                    }
                    else
                    {
                        feed.Atom ??= new AtomFeed();
                        target = feed.Atom;
                    }
                    targetFeed = feed.Atom;
                    targetEntry = feed.CurrentItem?.Atom;
                }

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Common

                    case "author": //Names one author of the feed entry.
                        {
                            //Get author properties
                            target.Author = new FeedPerson();
                            var authorElements = await reader.AllSubTreeElements().ConfigureAwait(false);
                            foreach (var element in authorElements)
                            {
                                switch (element.Key)
                                {
                                    case "email":
                                        {
                                            try
                                            {
                                                //Attempt to parse author email
                                                target.Author.Email = element.Value.ToMailAddress();
                                            }
                                            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is FormatException)
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}, {ex.Message}");
                                            }
                                            break;
                                        }
                                    case "name": target.Author.Name = element.Value; break;
                                    case "uri":
                                        {
                                            try
                                            {
                                                //Attempt to parse author URI
                                                target.Author.Uri = new Uri(element.Value);
                                            }
                                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}, {ex.Message}");
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            //Unknown node
                                            SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, element.Value, element.Key);
                                            break;
                                        }
                                }
                            }
                            break;
                        }

                    case "category": //One or more categories that the feed/entry belongs to.
                        {
                            //Verify link node
                            if (reader.HasAttributes && reader.GetAttribute("term") != null)
                            {
                                //Parse and add category to feed/entry catergories list
                                var category = new FeedCategory()
                                {
                                    Category = reader.GetAttribute("domain"),
                                    Label = reader.GetAttribute("label")
                                };

                                //Attempt to get category scheme
                                var scheme = reader.GetAttribute("scheme");
                                if (scheme != null)
                                {
                                    try
                                    {
                                        //Attempt to parse category scheme URI
                                        category.Scheme = new Uri(scheme);
                                    }
                                    catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                    {
                                        //Unknown node format
                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, scheme, $"Node: scheme, {ex.Message}");
                                    }
                                }

                                //Add category to categories list
                                target.Categories ??= new List<FeedCategory>();
                                target.Categories.Add(category);
                            }
                            else
                            {
                                //Missing href attribute
                                SetParseError(ParseErrorType.MissingAttribute, nodeInfo, feed, null, "term");
                            }
                            break;
                        }

                    case "contributor": //Name of one or more contributors to the feed entry.
                        {
                            //Init
                            var contributor = new FeedPerson();

                            //Get contributor properties
                            var contributorElements = await reader.AllSubTreeElements().ConfigureAwait(false);
                            foreach (var element in contributorElements)
                            {
                                switch (element.Key)
                                {
                                    case "email":
                                        {
                                            try
                                            {
                                                //Attempt to parse contributor email
                                                contributor.Email = element.Value.ToMailAddress();
                                            }
                                            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is FormatException)
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}, {ex.Message}");
                                            }
                                            break;
                                        }
                                    case "name": contributor.Name = element.Value; break;
                                    case "uri":
                                        {
                                            try
                                            {
                                                //Attempt to parse author URI
                                                contributor.Uri = new Uri(element.Value);
                                            }
                                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}, {ex.Message}");
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            //Unknown node
                                            SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, element.Value, element.Key);
                                            break;
                                        }
                                }
                            }

                            //Add contributor to contributors list
                            target.Contributors ??= new List<FeedPerson>();
                            target.Contributors.Add(contributor);
                            break;
                        }

                    case "entry": //Feed entry start, add new feed item to feed.
                        {
                            //Add new item
                            if (feed.CurrentParseType == ParseType.Feed) feed.AddItem();
                            break;
                        }

                    case "id": //Identifies the feed/entry using a universally unique and permanent URI.
                        {
                            //Get id
                            var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);

                            try
                            {
                                //Attempt to parse id URI
                                target.Id = new Uri(content);
                            }
                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                            }
                            break;
                        }

                    case "link": //Link to the referenced resource (typically a Web page)
                        {
                            //Verify link node
                            if (reader.HasAttributes && reader.GetAttribute("href") != null)
                            {
                                //Init
                                var link = new FeedLink();

                                //Get link attributes
                                while (reader.MoveToNextAttribute())
                                {
                                    //Attempt to parse attribute
                                    switch (reader.LocalName)
                                    {
                                        case "href":
                                            {
                                                try
                                                {
                                                    //Attempt to parse link href
                                                    link.Url = new Uri(reader.Value);
                                                }
                                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                                {
                                                    //Unknown node format
                                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}, {ex.Message}");
                                                }
                                                break;
                                            }
                                        case "hreflang":
                                            {
                                                try
                                                {
                                                    //Attempt to parse link hrefLang
                                                    link.Language = CultureInfo.GetCultureInfo(reader.Value);
                                                }
                                                catch (Exception ex) when (ex is ArgumentException || ex is CultureNotFoundException)
                                                {
                                                    //Unknown node format
                                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}, {ex.Message}");
                                                }
                                                break;
                                            }
                                        case "length":
                                            {
                                                //Attempt to parse link length
                                                if (long.TryParse(reader.Value, out var length))
                                                {
                                                    link.Length = length;
                                                }
                                                else
                                                {
                                                    //Unknown node format
                                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}");
                                                }
                                                break;
                                            }
                                        case "rel":
                                            {
                                                //Attempt to parse link rel
                                                switch (reader.Value)
                                                {
                                                    case "alternate": link.Type = FeedLinkType.Alternate; break;
                                                    case "enclosure": link.Type = FeedLinkType.Enclosure; break;
                                                    case "related": link.Type = FeedLinkType.Related; break;
                                                    case "self": link.Type = FeedLinkType.Self; break;
                                                    case "via": link.Type = FeedLinkType.Via; break;
                                                    default: SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}"); break;
                                                }
                                                break;
                                            }
                                        case "title": link.Text = reader.Value; break;
                                        case "type": link.MediaType = reader.Value; break;
                                        default:
                                            {
                                                //Unknown node
                                                SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, reader.Value, reader.LocalName);
                                                break;
                                            }
                                    }
                                }

                                //Add link to links collection
                                target.Links ??= new List<FeedLink>();
                                target.Links.Add(link);
                            }
                            else
                            {
                                //Missing href attribute
                                SetParseError(ParseErrorType.MissingAttribute, nodeInfo, feed, null, "href");
                            }
                            break;
                        }

                    case "rights": //Conveys information about rights, e.g. copyrights, held in and over the feed.
                        {
                            //Attemp to parse rights
                            target.Rights = new FeedText() { Type = reader.GetAttribute("type") };
                            target.Rights.Text = await reader.ReadStartElementAndContentAsStringAsync(target.Rights.Type).ConfigureAwait(false);
                            break;
                        }

                    case "title": //The name of the feed/entry.
                        {
                            //Attemp to parse title
                            target.Title = new FeedText() { Type = reader.GetAttribute("type") };
                            target.Title.Text = await reader.ReadStartElementAndContentAsStringAsync(target.Title.Type).ConfigureAwait(false);
                            break;
                        }

                    case "updated": //Indicates the last time the feed/entry was modified in a significant way.
                        {
                            //Init
                            var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);

                            //Attempt to parse updated date
                            if (DateTime.TryParse(content, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var updated))
                                target.Updated = updated;
                            else
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            break;
                        }

                    #endregion Common

                    #region Feed

                    case "icon": //Identifies a small image which provides iconic visual identification for the feed.
                        {
                            if (targetFeed != null)
                            {
                                //Get icon
                                var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);

                                try
                                {
                                    //Attempt to parse icon URI
                                    targetFeed.Icon = new Uri(content);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                                }
                            }
                            else
                            {
                                //Feed Atom object missing
                                throw new ArgumentNullException("Feed.Atom");
                            }
                            break;
                        }

                    case "generator": //Indicating the program used to generate the feed.
                        {
                            if (targetFeed != null)
                            {
                                //Init
                                targetFeed.Generator = new FeedGenerator();

                                //Attempt to parse optional attributes
                                var uri = reader.GetAttribute("uri");
                                if (uri != null)
                                {
                                    try
                                    {
                                        //Attempt to parse generator uri
                                        targetFeed.Generator.Uri = new Uri(uri);
                                    }
                                    catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                    {
                                        //Unknown node format
                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, uri, $"Node: uri, {ex.Message}");
                                    }
                                }
                                targetFeed.Generator.Version = reader.GetAttribute("version");

                                //Attempt to parse feed generator
                                targetFeed.Generator.Generator = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);
                            }
                            else
                            {
                                //Feed Atom object missing
                                throw new ArgumentNullException("Feed.Atom");
                            }
                            break;
                        }

                    case "logo": //Identifies a larger image which provides visual identification for the feed.
                        {
                            if (targetFeed != null)
                            {
                                //Init
                                targetFeed.Logo = new FeedImage();

                                //Get logo
                                var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);

                                try
                                {
                                    //Attempt to parse logo URI
                                    targetFeed.Logo.Url = new Uri(content);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                                }
                            }
                            else
                            {
                                //Feed Atom object missing
                                throw new ArgumentNullException("Feed.Atom");
                            }
                            break;
                        }

                    case "subtitle": //Contains a human-readable description or subtitle for the feed.
                        {
                            if (targetFeed != null)
                            {
                                //Attemp to parse subtitle
                                targetFeed.Subtitle = new FeedText() { Type = reader.GetAttribute("type") };
                                targetFeed.Subtitle.Text = await reader.ReadStartElementAndContentAsStringAsync(targetFeed.Subtitle.Type).ConfigureAwait(false);
                            }
                            else
                            {
                                //Feed Atom object missing
                                throw new ArgumentNullException("Feed.Atom");
                            }
                            break;
                        }

                    #endregion Feed

                    #region Entry

                    case "content": //Contains or links to the complete content of the entry.
                        {
                            if (targetEntry != null)
                            {
                                //Attemp to parse content
                                targetEntry.Content = new FeedContent() { Type = reader.GetAttribute("type") };
                                targetEntry.Content.Text = await reader.ReadStartElementAndContentAsStringAsync(targetEntry.Content.Type).ConfigureAwait(false);

                                //Attempt to get content src
                                var src = reader.GetAttribute("src");
                                if (src != null)
                                {
                                    try
                                    {
                                        //Attempt to parse content src
                                        targetEntry.Content.Source = new Uri(src);
                                    }
                                    catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                    {
                                        //Unknown node format
                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, src, $"Node: src, {ex.Message}");
                                    }
                                }
                            }
                            else
                            {
                                if (feed.Type == FeedType.Atom)
                                    //Feed item object missing
                                    throw new ArgumentNullException("Feed.CurrentItem");
                                else
                                    //Feed CurrentItem Atom object missing
                                    throw new ArgumentNullException("Feed.CurrentItem.Atom");
                            }
                            break;
                        }

                    case "published":
                        {
                            if (targetEntry != null)
                            {
                                //Get published
                                var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);

                                //Attemp to parser published
                                if (DateTime.TryParse(content, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var published))
                                    targetEntry.Published = published;
                                else
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            }
                            else
                            {
                                if (feed.Type == FeedType.Atom)
                                    //Feed item object missing
                                    throw new ArgumentNullException("Feed.CurrentItem");
                                else
                                    //Feed CurrentItem Atom object missing
                                    throw new ArgumentNullException("Feed.CurrentItem.Atom");
                            }
                            break;
                        }

                    case "source":
                        {
                            if (targetEntry != null)
                            {
                                //Init
                                targetEntry.Source = new FeedLink();

                                //Get source properties
                                var sourceElements = await reader.AllSubTreeElements().ConfigureAwait(false);
                                foreach (var element in sourceElements)
                                {
                                    switch (element.Key)
                                    {
                                        case "id":
                                            {
                                                try
                                                {
                                                    //Attempt to parse id URI
                                                    targetEntry.Source.Url = new Uri(element.Value);
                                                }
                                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                                {
                                                    //Unknown node format
                                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}, {ex.Message}");
                                                }
                                                break;
                                            }
                                        case "title":
                                            {
                                                //Set title text
                                                targetEntry.Source.Text = element.Value;
                                                break;
                                            }
                                        case "updated":
                                            {
                                                //Attempt to parse updated date
                                                if (DateTime.TryParse(element.Value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var updated))
                                                    targetEntry.Source.Updated = updated;
                                                else
                                                    //Unknown node format
                                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}");
                                                break;
                                            }
                                        default:
                                            {
                                                //Unknown node
                                                SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, element.Value, element.Key);
                                                break;
                                            }
                                    }
                                }
                            }
                            else
                            {
                                if (feed.Type == FeedType.Atom)
                                    //Feed item object missing
                                    throw new ArgumentNullException("Feed.CurrentItem");
                                else
                                    //Feed CurrentItem Atom object missing
                                    throw new ArgumentNullException("Feed.CurrentItem.Atom");
                            }
                            break;
                        }

                    case "summary": //Conveys a short summary, abstract, or excerpt of the entry.
                        {
                            if (targetEntry != null)
                            {
                                //Attemp to parse summary
                                targetEntry.Summary = new FeedText() { Type = reader.GetAttribute("type") };
                                targetEntry.Summary.Text = await reader.ReadStartElementAndContentAsStringAsync(targetEntry.Summary.Type).ConfigureAwait(false);
                            }
                            else
                            {
                                if (feed.Type == FeedType.Atom)
                                    //Feed item object missing
                                    throw new ArgumentNullException("Feed.CurrentItem");
                                else
                                    //Feed CurrentItem Atom object missing
                                    throw new ArgumentNullException("Feed.CurrentItem.Atom");
                            }
                            break;
                        }

                    #endregion Entry

                    default: //Unknown feed/entry node, continue to next.
                        {
                            result = false;
                            if (root) SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            break;
                        }
                }
            }
            else if (result = reader.NodeType == XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "entry": //Feed entry end, close current feed item.
                        {
                            feed.CloseItem();
                            break;
                        }
                }
            }

            //Return result
            return result;
        }
    }
}