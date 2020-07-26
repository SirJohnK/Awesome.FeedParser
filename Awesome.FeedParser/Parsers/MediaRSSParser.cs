using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Models.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Media RSS feed nodes.
    /// </summary>
    internal sealed class MediaRSSParser : BaseParser
    {
        /// <summary>
        /// Media RSS Namespace URI.
        /// </summary>
        public static string Namespace { get; } = @"http://search.yahoo.com/mrss/";

        /// <summary>
        /// Parser lazy loaded instance.
        /// </summary>
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new MediaRSSParser());

        /// <summary>
        /// Private constructor to prevent external initalization.
        /// </summary>
        private MediaRSSParser()
        {
        }

        /// <summary>
        /// Main Media RSS parsing method.
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
                MediaInformation targetInformation;
                NodeInformation nodeInfo = reader.NodeInformation();

                //Add Media RSS to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.MediaRSS;

                //Set feed target
                if (feed.CurrentItem != null)
                {
                    feed.CurrentItem.Media ??= new MediaItem();
                    if (!string.Equals(reader.LocalName, "group") && !string.Equals(reader.LocalName, "content"))
                        //Get and set feed item current media information
                        targetInformation = feed.CurrentItem.Media.CurrentInformation;
                    else
                        //Set default target
                        targetInformation = new MediaInformation();
                }
                else
                {
                    //Get and set feed media information
                    targetInformation = feed.MediaInformation ??= new MediaInformation();
                }

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Common

                    case "backLinks": //Allows inclusion of all the URLs pointing to a media object.
                        {
                            //Init
                            targetInformation.backLinks ??= new List<Uri>();

                            //Attempt to parse comments
                            if (!reader.IsEmptyElement)
                            {
                                var subtree = reader.ReadSubtree();
                                while (subtree.ReadToFollowing("backLink", reader.NamespaceURI))
                                {
                                    var backLink = await subtree.ReadStartElementAndContentAsStringAsync();
                                    if (!string.IsNullOrWhiteSpace(backLink))
                                    {
                                        try
                                        {
                                            //Attempt to parse backLink url
                                            targetInformation.backLinks.Add(new Uri(backLink));
                                        }
                                        catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                        {
                                            //Unknown node format
                                            SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, backLink, $"Node: backLink, {ex.Message}");
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    case "category": //Allows tags or categories to be set for the media.
                        {
                            //Init
                            targetInformation.categories ??= new List<FeedCategory>();

                            //Attemp to parse category
                            var scheme = reader.GetAttribute("scheme");
                            var category = new FeedCategory() { Label = reader.GetAttribute("label") };
                            category.Category = await reader.ReadStartElementAndContentAsStringAsync();
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

                            //Add category to target categories
                            targetInformation.categories.Add(category);
                            break;
                        }

                    case "credit": //Allows credit to be given to people and entities for the creation of the media.
                        {
                            //Init
                            targetInformation.credits ??= new List<MediaCredit>();

                            //Attemp to parse credit
                            var scheme = reader.GetAttribute("scheme");
                            var credit = new MediaCredit() { Role = reader.GetAttribute("role") };
                            credit.Name = await reader.ReadStartElementAndContentAsStringAsync();
                            if (scheme != null)
                            {
                                try
                                {
                                    //Attempt to parse credit scheme URI
                                    credit.Scheme = new Uri(scheme);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, scheme, $"Node: scheme, {ex.Message}");
                                }
                            }

                            //Add credit to target credits
                            targetInformation.credits.Add(credit);
                            break;
                        }

                    case "comments": //Allows inclusion of all the comments a media object has received.
                        {
                            //Init
                            targetInformation.comments ??= new List<string>();

                            //Attempt to parse comments
                            if (!reader.IsEmptyElement)
                            {
                                var subtree = reader.ReadSubtree();
                                while (subtree.ReadToFollowing("comment", reader.NamespaceURI))
                                {
                                    var comment = await subtree.ReadStartElementAndContentAsStringAsync();
                                    if (!string.IsNullOrWhiteSpace(comment))
                                        targetInformation.comments.Add(comment);
                                }
                            }
                            break;
                        }

                    case "community": //Allows inclusion of the user perception about a media object in the form of view count, ratings and tags.
                        {
                            //Init
                            var subtree = reader.ReadSubtree();
                            var community = new MediaCommunity();

                            //Attempt to read and parse community
                            if (!reader.IsEmptyElement)
                            {
                                while (await subtree.ReadAsync())
                                {
                                    switch (subtree.LocalName)
                                    {
                                        case "community": break;
                                        case "starRating":
                                            {
                                                //Get starRating attributes
                                                if (subtree.HasAttributes)
                                                {
                                                    while (subtree.MoveToNextAttribute())
                                                    {
                                                        //Attempt to parse attribute
                                                        switch (subtree.LocalName)
                                                        {
                                                            case "average":
                                                                {
                                                                    //Attempt to parse star rating average
                                                                    var value = subtree.Value.Replace(",", ".");
                                                                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var average))
                                                                    {
                                                                        community.RatingAverage = average;
                                                                    }
                                                                    else
                                                                    {
                                                                        //Unknown node format
                                                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, subtree.Value, $"Node: {subtree.LocalName}");
                                                                    }
                                                                    break;
                                                                }
                                                            case "count":
                                                                {
                                                                    //Attempt to parse star rating count
                                                                    if (int.TryParse(subtree.Value, out var count))
                                                                    {
                                                                        community.RatingCount = count;
                                                                    }
                                                                    else
                                                                    {
                                                                        //Unknown node format
                                                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, subtree.Value, $"Node: {subtree.LocalName}");
                                                                    }
                                                                    break;
                                                                }
                                                            case "max":
                                                                {
                                                                    //Attempt to parse star rating max
                                                                    if (int.TryParse(subtree.Value, out var max))
                                                                    {
                                                                        community.RatingMax = max;
                                                                    }
                                                                    else
                                                                    {
                                                                        //Unknown node format
                                                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, subtree.Value, $"Node: {subtree.LocalName}");
                                                                    }
                                                                    break;
                                                                }
                                                            case "min":
                                                                {
                                                                    //Attempt to parse star rating min
                                                                    if (int.TryParse(subtree.Value, out var min))
                                                                    {
                                                                        community.RatingMin = min;
                                                                    }
                                                                    else
                                                                    {
                                                                        //Unknown node format
                                                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, subtree.Value, $"Node: {subtree.LocalName}");
                                                                    }
                                                                    break;
                                                                }
                                                            default:
                                                                {
                                                                    //Unknown node
                                                                    SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, subtree.Value, subtree.LocalName);
                                                                    break;
                                                                }
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        case "statistics":
                                            {
                                                //Get statistics attributes
                                                if (subtree.HasAttributes)
                                                {
                                                    while (subtree.MoveToNextAttribute())
                                                    {
                                                        //Attempt to parse attribute
                                                        switch (subtree.LocalName)
                                                        {
                                                            case "views":
                                                                {
                                                                    //Attempt to parse statistics views
                                                                    if (int.TryParse(subtree.Value, out var views))
                                                                    {
                                                                        community.Views = views;
                                                                    }
                                                                    else
                                                                    {
                                                                        //Unknown node format
                                                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, subtree.Value, $"Node: {subtree.LocalName}");
                                                                    }
                                                                    break;
                                                                }
                                                            case "favorites":
                                                                {
                                                                    //Attempt to parse star rating favorites
                                                                    if (int.TryParse(subtree.Value, out var favorites))
                                                                    {
                                                                        community.Favorites = favorites;
                                                                    }
                                                                    else
                                                                    {
                                                                        //Unknown node format
                                                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, subtree.Value, $"Node: {subtree.LocalName}");
                                                                    }
                                                                    break;
                                                                }
                                                            default:
                                                                {
                                                                    //Unknown node
                                                                    SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, subtree.Value, subtree.LocalName);
                                                                    break;
                                                                }
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        case "tags":
                                            {
                                                //Init
                                                community.tags ??= new List<(string Tag, int Weight)>();

                                                //Attempt to parse community tags
                                                var content = await subtree.ReadStartElementAndContentAsStringAsync();
                                                var tags = content.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(tag => tag.Trim());
                                                foreach (var tag in tags)
                                                {
                                                    var parts = tag.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(part => part.Trim()).ToList();
                                                    if (parts.Count > 1 && int.TryParse(parts[1], out var weight))
                                                        community.tags.Add((parts[0], weight));
                                                    else
                                                        community.tags.Add((parts[0], 1));
                                                }
                                                break;
                                            }
                                        default:
                                            {
                                                //Unknown node
                                                SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, reader.Value, reader.LocalName);
                                                break;
                                            }
                                    }
                                }
                            }

                            //Set media community information
                            targetInformation.Community = community;
                            break;
                        }

                    case "copyright": //Provides a means to specify the copyright if no other copyright module is used.
                        {
                            break;
                        }

                    case "description": //Description of the media object.
                        {
                            break;
                        }

                    case "embed": //Allows inclusion of player-specific embed code for a player to play any video.
                        {
                            //Init
                            var embed = new MediaEmbed();

                            //Attempt to parse embed
                            var heightValue = reader.GetAttribute("height");
                            var urlValue = reader.GetAttribute("url");
                            var widthValue = reader.GetAttribute("width");

                            if (heightValue != null)
                            {
                                //Attempt to parse embed height
                                if (int.TryParse(heightValue, out var height))
                                {
                                    embed.Height = height;
                                }
                                else
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, heightValue, $"Node: height");
                                }
                            }

                            if (urlValue != null)
                            {
                                try
                                {
                                    //Attempt to parse embed url
                                    embed.Url = new Uri(urlValue);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, urlValue, $"Node: url, {ex.Message}");
                                }
                            }

                            if (widthValue != null)
                            {
                                //Attempt to parse embed width
                                if (int.TryParse(widthValue, out var width))
                                {
                                    embed.Width = width;
                                }
                                else
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, widthValue, $"Node: width");
                                }
                            }

                            //Attempt to parse embed parameters
                            if (!reader.IsEmptyElement)
                            {
                                var subtree = reader.ReadSubtree();
                                embed.parameters = new Dictionary<string, string>();
                                while (subtree.ReadToFollowing("param", reader.NamespaceURI))
                                {
                                    var name = subtree.GetAttribute("name");
                                    if (name != null) embed.parameters.Add(name, await subtree.ReadStartElementAndContentAsStringAsync());
                                }
                            }

                            //Set media embed
                            targetInformation.Embed = embed;
                            break;
                        }

                    case "hash": //A "md5" or "sha-1" hash of the media can be used to help verify the integrity and/or look for duplicates.
                        {
                            //Init
                            targetInformation.hash ??= new List<FeedText>();

                            //Attempt to parse hash
                            var hash = new FeedText() { Type = reader.GetAttribute("algo") };
                            hash.Text = await reader.ReadStartElementAndContentAsStringAsync();
                            targetInformation.hash.Add(hash);
                            break;
                        }

                    case "keywords": //A short list or comma separated words and phrases describing the media content.
                        {
                            break;
                        }

                    case "license": //Specify the machine-readable license associated with the content.
                        {
                            //Init
                            var license = new MediaLegal() { Type = reader.GetAttribute("type") };

                            //Attempt to parse license
                            var href = reader.GetAttribute("href");
                            if (href != null)
                            {
                                try
                                {
                                    //Attempt to parse license href
                                    license.Url = new Uri(href);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, href, $"Node: href, {ex.Message}");
                                }
                            }

                            //Attempt to parse license text
                            if (!reader.IsEmptyElement)
                                license.Text = await reader.ReadStartElementAndContentAsStringAsync();

                            //Set target license
                            targetInformation.License = license;
                            break;
                        }

                    case "location": //Geographical information about various locations captured in the content of a media object. (Conforms to geoRSS)
                        {
                            break;
                        }

                    case "peerLink": //P2P link.
                        {
                            //Init
                            var link = new FeedLink() { MediaType = reader.GetAttribute("type") };

                            //Attempt to parse peerLink
                            var href = reader.GetAttribute("href");
                            if (href != null)
                            {
                                try
                                {
                                    //Attempt to parse peerlink href
                                    link.Url = new Uri(href);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, href, $"Node: href, {ex.Message}");
                                }
                            }

                            //Set target peer link
                            targetInformation.PeerLink = link;
                            break;
                        }

                    case "player": //Required if <content> did not specify the "url" attribute. This tag allows the media object to be accessed through a web browser or media console.
                        {
                            //Init
                            var player = new MediaEmbed();

                            //Get player attributes
                            while (reader.MoveToNextAttribute())
                            {
                                //Attempt to parse attribute
                                switch (reader.LocalName)
                                {
                                    case "height": //Height of the browser window that the URL should be opened in.
                                        {
                                            //Attempt to parse height
                                            if (int.TryParse(reader.Value, out var height))
                                            {
                                                player.Height = height;
                                            }
                                            else
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}");
                                            }
                                            break;
                                        }
                                    case "url": //URL of the player console that plays the media.
                                        {
                                            try
                                            {
                                                //Attempt to parse player url
                                                player.Url = new Uri(reader.Value);
                                            }
                                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}, {ex.Message}");
                                            }
                                            break;
                                        }
                                    case "width": //Width of the browser window that the URL should be opened in.
                                        {
                                            //Attempt to parse width
                                            if (int.TryParse(reader.Value, out var width))
                                            {
                                                player.Width = width;
                                            }
                                            else
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}");
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            //Unknown node
                                            SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, reader.Value, reader.LocalName);
                                            break;
                                        }
                                }
                            }

                            //Set target player
                            targetInformation.Player = player;
                            break;
                        }

                    case "price": //Pricing information about a media object.
                        {
                            //Init
                            targetInformation.prices ??= new List<MediaPrice>();
                            var price = new MediaPrice() { Currency = reader.GetAttribute("currency"), Info = reader.GetAttribute("info") };

                            //Attempt to parse price
                            var priceValue = reader.GetAttribute("price");
                            if (!string.IsNullOrWhiteSpace(priceValue))
                            {
                                priceValue = priceValue.Replace(",", ".");
                                if (decimal.TryParse(priceValue, NumberStyles.Currency, CultureInfo.InvariantCulture, out var value)) price.Price = value;
                            }

                            //Attempt to parse price type
                            var type = reader.GetAttribute("type");
                            if (type != null)
                            {
                                switch (type)
                                {
                                    case "package": price.Type = MediaPriceType.Package; break;
                                    case "purchase": price.Type = MediaPriceType.Purchase; break;
                                    case "rent": price.Type = MediaPriceType.Rent; break;
                                    case "subscription": price.Type = MediaPriceType.Subscription; break;
                                    default: SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, type, $"Node: type"); break;
                                }
                            }

                            //Add price to prices
                            targetInformation.prices.Add(price);
                            break;
                        }

                    case "rating": //Allows the permissible audience to be declared.
                        {
                            //Attempt to parse rating
                            var rating = new MediaRating();
                            var scheme = reader.GetAttribute("scheme");
                            if (scheme != null)
                            {
                                try
                                {
                                    //Attempt to parse rating scheme URI
                                    rating.Scheme = new Uri(scheme);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, scheme, $"Node: scheme, {ex.Message}");
                                }
                            }
                            rating.Rating = await reader.ReadStartElementAndContentAsStringAsync();

                            //Set target rating
                            targetInformation.Rating = rating;
                            break;
                        }

                    case "responses": //Allows inclusion of a list of all media responses a media object has received.
                        {
                            //Init
                            targetInformation.responses ??= new List<string>();

                            //Attemp to parse responses
                            if (!reader.IsEmptyElement)
                            {
                                var subtree = reader.ReadSubtree();
                                while (subtree.ReadToFollowing("response", reader.NamespaceURI))
                                {
                                    var response = await subtree.ReadStartElementAndContentAsStringAsync();
                                    if (!string.IsNullOrWhiteSpace(response))
                                        targetInformation.responses.Add(response);
                                }
                            }
                            break;
                        }

                    case "restriction": //Allows complete control of payback of the media using a wide range of criteria.
                        {
                            break;
                        }

                    case "rights": //Rights information of a media object.
                        {
                            break;
                        }

                    case "status": //Specify the status of a media object.
                        {
                            //Init
                            var state = reader.GetAttribute("state");
                            var status = new MediaStatus() { Reason = reader.GetAttribute("reason") };

                            //Attempt to parse status state
                            switch (state)
                            {
                                case "active": status.State = MediaStatusState.Active; break;
                                case "blocked": status.State = MediaStatusState.Blocked; break;
                                case "deleted": status.State = MediaStatusState.Deleted; break;
                                default: SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, state, $"Node: state"); break;
                            }

                            //Set media status
                            targetInformation.Status = status;
                            break;
                        }

                    case "scenes": //Specifies various scenes within a media object.
                        {
                            break;
                        }

                    case "subTitle": //Subtitle/CC link.
                        {
                            //Init
                            targetInformation.subtitles ??= new List<FeedLink>();
                            var subtitle = new FeedLink() { MediaType = reader.GetAttribute("type") };

                            //Attempt to parse subTitle
                            var href = reader.GetAttribute("href");
                            if (href != null)
                            {
                                try
                                {
                                    //Attempt to parse subTitle href
                                    subtitle.Url = new Uri(href);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, href, $"Node: href, {ex.Message}");
                                }
                            }

                            var lang = reader.GetAttribute("lang");
                            try
                            {
                                //Attempt to parse subTitle lang
                                subtitle.Language = CultureInfo.GetCultureInfo(lang);
                            }
                            catch (Exception ex) when (ex is ArgumentException || ex is CultureNotFoundException)
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, lang, $"Node: lang, {ex.Message}");
                            }

                            //Add subtitle to target subitles
                            targetInformation.subtitles.Add(subtitle);
                            break;
                        }

                    case "text": //Allows text to be included with the media object.
                        {
                            //Init
                            targetInformation.texts ??= new List<MediaText>();

                            //Attempt to parse text
                            var lang = reader.GetAttribute("lang");
                            var start = reader.GetAttribute("start");
                            var end = reader.GetAttribute("end");
                            var text = new MediaText() { Type = reader.GetAttribute("type") };
                            if (lang != null)
                            {
                                try
                                {
                                    //Attempt to parse text lang
                                    text.Language = CultureInfo.GetCultureInfo(lang);
                                }
                                catch (Exception ex) when (ex is ArgumentException || ex is CultureNotFoundException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, lang, $"Node: lang, {ex.Message}");
                                }
                            }
                            if (start != null)
                            {
                                //Attempt to parse text start
                                if (TimeSpan.TryParse(start, out var startTime))
                                {
                                    text.StartTime = startTime;
                                }
                                else
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, start, $"Node: start");
                                }
                            }
                            if (end != null)
                            {
                                //Attempt to parse text end
                                if (TimeSpan.TryParse(end, out var endTime))
                                {
                                    text.EndTime = endTime;
                                }
                                else
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, end, $"Node: end");
                                }
                            }
                            text.Text = await reader.ReadStartElementAndContentAsStringAsync();

                            //Add text to target texts
                            targetInformation.texts.Add(text);
                            break;
                        }

                    case "thumbnail": //Image to represent the media.
                        {
                            break;
                        }

                    case "title": //The title of the media object.
                        {
                            break;
                        }

                    #endregion Common

                    #region Item

                    case "content": //Used to specify the group/item enclosed media content.
                        {
                            if (feed.CurrentItem?.Media != null)
                            {
                                //Add new media content
                                var content = feed.CurrentItem.Media.AddContent(reader.IsEmptyElement);

                                //Get content attributes
                                while (reader.MoveToNextAttribute())
                                {
                                    //Attempt to parse attribute
                                    switch (reader.LocalName)
                                    {
                                        case "bitrate": //Kilobits per second rate of media.
                                            {
                                                //Attempt to parse content bitrate
                                                if (int.TryParse(reader.Value, out var bitrate))
                                                {
                                                    content.Bitrate = bitrate;
                                                }
                                                else
                                                {
                                                    //Unknown node format
                                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}");
                                                }
                                                break;
                                            }
                                        case "channels": //Number of audio channels in the media object.
                                            {
                                                break;
                                            }
                                        case "duration": //Number of seconds the media object plays.
                                            {
                                                break;
                                            }
                                        case "expression": //Determines if the object is a sample or the full version of the object, or even if it is a continuous stream (sample | full | nonstop).
                                            {
                                                //Attempt to parse content expression
                                                switch (reader.Value)
                                                {
                                                    case "full": content.Expression = MediaContentExpression.Full; break;
                                                    case "nonstop": content.Expression = MediaContentExpression.Nonstop; break;
                                                    case "sample": content.Expression = MediaContentExpression.Sample; break;
                                                    default: SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}"); break;
                                                }
                                                break;
                                            }
                                        case "fileSize": //Number of bytes of the media object.
                                            {
                                                //Attempt to parse content filesize
                                                if (long.TryParse(reader.Value, out var fileSize))
                                                {
                                                    content.FileSize = fileSize;
                                                }
                                                else
                                                {
                                                    //Unknown node format
                                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}");
                                                }
                                                break;
                                            }
                                        case "framerate": //Number of frames per second for the media object.
                                            {
                                                break;
                                            }
                                        case "height": //Height of the media object.
                                            {
                                                break;
                                            }
                                        case "isDefault": //Determines if this is the default object that should be used for the <media:group>.
                                            {
                                                break;
                                            }
                                        case "lang": //Primary language encapsulated in the media object. (Language codes: RFC 3066)
                                            {
                                                break;
                                            }
                                        case "medium": //Type of object (image | audio | video | document | executable).
                                            {
                                                break;
                                            }
                                        case "samplingrate": //Number of samples per second taken to create the media object.
                                            {
                                                break;
                                            }
                                        case "type": //Standard MIME type of the object.
                                            {
                                                content.Type = reader.Value;
                                                break;
                                            }
                                        case "url": //Specify the direct URL to the media object.
                                            {
                                                try
                                                {
                                                    //Attempt to parse content url
                                                    content.Url = new Uri(reader.Value);
                                                }
                                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                                {
                                                    //Unknown node format
                                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, reader.Value, $"Node: {reader.LocalName}, {ex.Message}");
                                                }
                                                break;
                                            }
                                        case "width": //Width of the media object.
                                            {
                                                break;
                                            }
                                        default:
                                            {
                                                //Unknown node
                                                SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, reader.Value, reader.LocalName);
                                                break;
                                            }
                                    }
                                }
                            }
                            else
                                //Feed CurrentItem Media object missing
                                throw new ArgumentNullException("Feed.CurrentItem.Media");
                            break;
                        }

                    case "group": //Allows for grouping of item media representations.
                        {
                            //Add new media group
                            if (feed.CurrentParseType == ParseType.Item) feed.CurrentItem?.Media?.AddGroup();
                            break;
                        }

                    #endregion Item

                    default: //Unknown feed/item node, continue to next.
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
                    case "content": //Media content end, close current media content.
                        {
                            feed.CurrentItem?.Media?.CloseContent();
                            break;
                        }

                    case "group": //Media group end, close current media group.
                        {
                            feed.CurrentItem?.Media?.CloseGroup();
                            break;
                        }
                }
            }

            //Return result
            return result;
        }
    }
}