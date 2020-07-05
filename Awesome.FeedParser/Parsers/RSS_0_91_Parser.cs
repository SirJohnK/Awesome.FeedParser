using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for RSS 0.91 feed nodes.
    /// </summary>
    internal class RSS_0_91_Parser : BaseParser
    {
        //Parser lazy loaded instance
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RSS_0_91_Parser());

        //Protected constructor to prevent external initalization
        protected RSS_0_91_Parser()
        {
        }

        /// <summary>
        /// Main RSS 0.91 parsing method.
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
                    #region Required

                    case "channel": //Feed root node. (Ignored)
                        {
                            //Advance to next node
                            reader.ReadStartElement();
                            break;
                        }

                    case "description": //Phrase or sentence describing the feed/item.
                        {
                            target.Description = await reader.ReadElementContentAsStringAsync();
                            break;
                        }

                    case "language": //The language the feed is written in. (ISO 639)
                        {
                            //Get feed language
                            var content = await reader.ReadElementContentAsStringAsync();

                            try
                            {
                                //Attempt to set feed Language
                                feed.Language = CultureInfo.GetCultureInfo(content);
                            }
                            catch (Exception ex) when (ex is ArgumentException || ex is CultureNotFoundException)
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                            }
                            break;
                        }

                    case "link": //The URL to the HTML website corresponding to the feed/item.
                        {
                            //Get link
                            var content = await reader.ReadElementContentAsStringAsync();

                            try
                            {
                                //Attempt to parse link URL
                                target.Link = new Uri(content);
                            }
                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                            }
                            break;
                        }

                    case "title": //The name of the feed/item.
                        {
                            target.Title = await reader.ReadElementContentAsStringAsync();
                            break;
                        }

                    #endregion Required

                    #region Optional

                    #region Common

                    case "pubDate": //The publication date for the content in the feed/item.
                        {
                            //Get publication date
                            var content = await reader.ReadElementContentAsStringAsync();

                            //Attemp to parser publication date
                            if (DateTime.TryParse(content, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var pubDate))
                                target.PubDate = pubDate;
                            else
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            break;
                        }

                    #endregion Common

                    #region Feed

                    case "copyright": //Copyright notice for content in the feed.
                        {
                            feed.Copyright = await reader.ReadElementContentAsStringAsync();
                            break;
                        }

                    case "docs": //A URL that points to the documentation for the format used in the RSS file.
                        {
                            //Get docs
                            var content = await reader.ReadElementContentAsStringAsync();

                            try
                            {
                                //Attempt to parse docs URL
                                feed.Docs = new Uri(content);
                            }
                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                            }
                            break;
                        }

                    case "image": //Specifies a GIF, JPEG or PNG image that can be displayed with the feed.
                        {
                            //Get image properties
                            var image = feed.Image ?? new FeedImage();
                            var imageElements = await reader.AllSubTreeElements();
                            foreach (var element in imageElements)
                            {
                                switch (element.Key)
                                {
                                    case "title": image.Title = element.Value; break;
                                    case "description": image.Description = element.Value; break;
                                    case "url":
                                        {
                                            try
                                            {
                                                //Attempt to parse image URL
                                                image.Url = new Uri(element.Value);
                                            }
                                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}, {ex.Message}");
                                            }
                                            break;
                                        }
                                    case "link":
                                        {
                                            try
                                            {
                                                //Attempt to parse link URL
                                                image.Link = new Uri(element.Value);
                                            }
                                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}, {ex.Message}");
                                            }
                                            break;
                                        }
                                    case "width":
                                        {
                                            //Attempt to parse width
                                            if (int.TryParse(element.Value, out var width))
                                                image.Width = width;
                                            else
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}");
                                            break;
                                        }
                                    case "height":
                                        {
                                            //Attempt to parse width
                                            if (int.TryParse(element.Value, out var height))
                                                image.Height = height;
                                            else
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}");
                                            break;
                                        }
                                    default:
                                        {
                                            //Unknown node
                                            SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed, element.Value, $"Node: {element.Key}");
                                            break;
                                        }
                                }
                            }
                            feed.Image = image;
                            await reader.SkipAsync();
                            break;
                        }

                    case "lastBuildDate": //The last time the content of the feed changed.
                        {
                            //Init
                            var content = await reader.ReadElementContentAsStringAsync();

                            //Attempt to parse last build date
                            if (DateTime.TryParse(content, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var lastBuildDate))
                                feed.LastBuildDate = lastBuildDate;
                            else
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            break;
                        }

                    case "managingEditor": //Email address for person responsible for editorial content.
                        {
                            //Init
                            var content = await reader.ReadElementContentAsStringAsync();

                            try
                            {
                                //Attempt to parse managing editor
                                feed.ManagingEditor = content.ToMailAddress();
                            }
                            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is FormatException)
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                            }
                            break;
                        }

                    case "rating": //Protocol for Web Description Resources (POWDER)
                        {
                            feed.Rating = await reader.ReadElementContentAsStringAsync();
                            break;
                        }

                    case "skipDays": //Identifies days of the week during which the feed is not updated.
                        {
                            //Get skip days
                            var skipDays = WeekDays.None;
                            var skipDaysElements = await reader.AllSubTreeElements();
                            foreach (var element in skipDaysElements)
                            {
                                if (element.Key.Equals("day"))
                                {
                                    switch (element.Value)
                                    {
                                        case "Monday": skipDays |= WeekDays.Monday; break;
                                        case "Tuesday": skipDays |= WeekDays.Tuesday; break;
                                        case "Wednesday": skipDays |= WeekDays.Wednesday; break;
                                        case "Thursday": skipDays |= WeekDays.Thursday; break;
                                        case "Friday": skipDays |= WeekDays.Friday; break;
                                        case "Saturday": skipDays |= WeekDays.Saturday; break;
                                        case "Sunday": skipDays |= WeekDays.Sunday; break;
                                        default: SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}"); break;
                                    }
                                }
                                else
                                {
                                    //Unknown node
                                    SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed, element.Value, $"Node: {element.Key}");
                                }
                            }
                            feed.SkipDays = skipDays;
                            await reader.SkipAsync();
                            break;
                        }

                    case "skipHours": //Identifies the hours of the day during which the feed is not updated.
                        {
                            //Get skip hours
                            var skipHours = new List<int>();
                            var skipHoursElements = await reader.AllSubTreeElements();
                            foreach (var element in skipHoursElements)
                            {
                                if (element.Key.Equals("hour"))
                                    if (int.TryParse(element.Value, out var hour))
                                        skipHours.Add(hour);
                                    else
                                        //Unknown node format
                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}");
                                else
                                    //Unknown node
                                    SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed, element.Value, $"Node: {element.Key}");
                            }
                            feed.SkipHours = skipHours;
                            await reader.SkipAsync();
                            break;
                        }

                    case "textinput": //Specifies a text input box that can be displayed with the feed.
                        {
                            //Get text input properties
                            var textInput = feed.TextInput ?? new FeedTextInput();
                            var textInputElements = await reader.AllSubTreeElements();
                            foreach (var element in textInputElements)
                            {
                                switch (element.Key)
                                {
                                    case "description": textInput.Description = element.Value; break;
                                    case "link":
                                        {
                                            try
                                            {
                                                //Attempt to parse link URL
                                                textInput.Link = new Uri(element.Value);
                                            }
                                            catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                            {
                                                //Unknown node format
                                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, element.Value, $"Node: {element.Key}, {ex.Message}");
                                            }
                                            break;
                                        }
                                    case "name": textInput.Name = element.Value; break;
                                    case "title": textInput.Title = element.Value; break;
                                    default: SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed, element.Value, $"Node: {element.Key}"); break;
                                }
                            }
                            feed.TextInput = textInput;
                            await reader.SkipAsync();
                            break;
                        }

                    case "webMaster": //Email address for person responsible for technical issues relating to the feed.
                        {
                            //Init
                            var content = await reader.ReadElementContentAsStringAsync();

                            try
                            {
                                //Attempt to parse web master
                                feed.WebMaster = content.ToMailAddress();
                            }
                            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is FormatException)
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                            }
                            break;
                        }

                    case "item": //Feed item start, add new feed item to feed.
                        {
                            //Add new item
                            if (feed.CurrentParseType == ParseType.Feed) feed.AddItem();

                            //Advance to next node
                            reader.ReadStartElement();
                            break;
                        }

                    #endregion Feed

                    #endregion Optional

                    default: //Unknown feed/item node, if main root parser, log unknown node and continue to next.
                        {
                            if (root) SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            result = false;
                            break;
                        }
                }
            }
            else if (reader.NodeType == XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "item": //Feed item end, close current feed item.
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