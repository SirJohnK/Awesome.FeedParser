using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class RSS_2_0_Parser : IParser
    {
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RSS_2_0_Parser());

        private RSS_2_0_Parser()
        {
        }

        public async Task<bool> Parse(XmlReader reader, Feed feed)
        {
            //Init
            bool result;

            //Verify Element Node
            if (result = reader.NodeType == XmlNodeType.Element)
            {
                //Set common feed target
                ICommonFeed target = feed.CurrentItem ?? (ICommonFeed)feed;

                //Identify node name
                switch (reader.LocalName)
                {
                    //Required Nodes

                    #region Required

                    case "title": //The name of the feed.
                        target.Title = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "description": //Phrase or sentence describing the feed.
                        target.Description = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "link": //The URL to the HTML website corresponding to the feed.
                        target.Link = new Uri(await reader.ReadElementContentAsStringAsync());
                        break;

                    #endregion Required

                    //Optional Nodes

                    #region Optional

                    #region Common

                    case "pubDate": //The publication date for the content in the feed.
                        if (DateTime.TryParse(await reader.ReadElementContentAsStringAsync(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var pubDate))
                            target.PubDate = pubDate;
                        break;

                    case "category": //One or more categories that the feed/item belongs to.
                        var categories = target.Categories ?? new List<FeedCategory>();
                        categories.Add(new FeedCategory() { Domain = reader.GetAttribute("domain"), Category = await reader.ReadElementContentAsStringAsync() });
                        target.Categories = categories;
                        break;

                    #endregion Common

                    #region Feed

                    case "language": //The language the feed is written in. (ISO 639)
                        feed.Language = CultureInfo.GetCultureInfo(await reader.ReadElementContentAsStringAsync());
                        break;

                    case "copyright": //Copyright notice for content in the feed.
                        feed.Copyright = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "managingEditor": //Email address for person responsible for editorial content.
                        feed.ManagingEditor = new MailAddress(await reader.ReadElementContentAsStringAsync());
                        break;

                    case "webMaster": //Email address for person responsible for technical issues relating to the feed.
                        feed.WebMaster = new MailAddress(await reader.ReadElementContentAsStringAsync());
                        break;

                    case "lastBuildDate": //The last time the content of the feed changed.
                        if (DateTime.TryParse(await reader.ReadElementContentAsStringAsync(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var lastBuildDate))
                            feed.LastBuildDate = lastBuildDate;
                        break;

                    case "generator": //A string indicating the program used to generate the feed.
                        feed.Generator = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "docs": //A URL that points to the documentation for the format used in the RSS file.
                        feed.Docs = new Uri(await reader.ReadElementContentAsStringAsync());
                        break;

                    case "cloud": //Allows processes to register with a cloud to be notified of updates to the feed, implementing a lightweight publish-subscribe protocol for RSS feeds.
                        feed.Cloud = new FeedCloud()
                        {
                            Domain = reader.GetAttribute("domain"),
                            Path = reader.GetAttribute("path"),
                            Port = reader.GetAttribute("port"),
                            Protocol = reader.GetAttribute("protocol"),
                            RegisterProcedure = reader.GetAttribute("registerProcedure")
                        };
                        break;

                    case "ttl": //Number of minutes that indicates how long a feed can be cached before refreshing from the source.
                        if (double.TryParse(await reader.ReadElementContentAsStringAsync(), out var ttl))
                            feed.Ttl = TimeSpan.FromMinutes(ttl);
                        break;

                    case "image": //Specifies a GIF, JPEG or PNG image that can be displayed with the feed.
                        if (!reader.IsEmptyElement)
                        {
                            //Get image properties
                            var image = new FeedImage();
                            var properties = await reader.AllSubTreeElements();
                            foreach (var element in properties)
                            {
                                if (element.Key.Equals("title")) image.Title = element.Value;
                                else if (element.Key.Equals("description")) image.Description = element.Value;
                                else if (element.Key.Equals("url")) image.Url = new Uri(element.Value);
                                else if (element.Key.Equals("link")) image.Link = new Uri(element.Value);
                                else if (element.Key.Equals("width") && int.TryParse(element.Value, out var width)) image.Width = width;
                                else if (element.Key.Equals("height") && int.TryParse(element.Value, out var height)) image.Height = height;
                            }
                            feed.Image = image;
                            await reader.SkipAsync();
                        }
                        break;

                    case "rating": //Protocol for Web Description Resources (POWDER)
                        feed.Rating = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "textInput": //Specifies a text input box that can be displayed with the feed.
                        if (!reader.IsEmptyElement)
                        {
                            //Get text input properties
                            var textInput = new FeedTextInput();
                            var properties = await reader.AllSubTreeElements();
                            foreach (var element in properties)
                            {
                                if (element.Key.Equals("description")) textInput.Description = element.Value;
                                else if (element.Key.Equals("link")) textInput.Link = new Uri(element.Value);
                                else if (element.Key.Equals("name")) textInput.Name = element.Value;
                                else if (element.Key.Equals("title")) textInput.Title = element.Value;
                            }
                            feed.TextInput = textInput;
                            await reader.SkipAsync();
                        }
                        break;

                    case "skipHours": //Identifies the hours of the day during which the feed is not updated.
                        if (!reader.IsEmptyElement)
                        {
                            //Get skip hours
                            var skipHours = new List<int>();
                            var properties = await reader.AllSubTreeElements();
                            foreach (var element in properties)
                            {
                                if (element.Key.Equals("hour") && int.TryParse(element.Value, out var hour))
                                    skipHours.Add(hour);
                            }
                            feed.SkipHours = skipHours;
                            await reader.SkipAsync();
                        }
                        break;

                    case "skipDays": //Identifies days of the week during which the feed is not updated.
                        if (!reader.IsEmptyElement)
                        {
                            //Get skip days
                            var skipDays = WeekDays.None;
                            var properties = await reader.AllSubTreeElements();
                            foreach (var element in properties)
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
                                    }
                                }
                            }
                            feed.SkipDays = skipDays;
                            await reader.SkipAsync();
                        }
                        break;

                    case "item": //Feed item start, add new feed item to feed.
                        feed.AddItem();
                        result = false;
                        break;

                    #endregion Feed

                    #region Feed Item

                    case "author": //Email address of the author of the feed item.
                        if (feed.CurrentItem != null) feed.CurrentItem.Author = new MailAddress(await reader.ReadElementContentAsStringAsync());
                        break;

                    case "comments": //URL of a page for comments relating to the feed item.
                        if (feed.CurrentItem != null) feed.CurrentItem.Comments = new Uri(await reader.ReadElementContentAsStringAsync());
                        break;

                    case "enclosure": //Media object that is attached to the feed item.
                        if (feed.CurrentItem != null)
                        {
                            feed.CurrentItem.Enclosure = new FeedMedia()
                            {
                                Length = long.Parse(reader.GetAttribute("length")),
                                Type = reader.GetAttribute("type"),
                                Url = new Uri(reader.GetAttribute("url"))
                            };
                            result = false;
                        }
                        break;

                    case "guid": //A string/link that uniquely identifies the feed item.
                        if (feed.CurrentItem != null)
                        {
                            var isPermaLink = reader.GetAttribute("isPermaLink");
                            feed.CurrentItem.Guid = new FeedGuid() { Guid = await reader.ReadElementContentAsStringAsync() };
                            if (bool.TryParse(isPermaLink ?? bool.FalseString, out var isLink))
                                feed.CurrentItem.Guid.IsPermaLink = isLink;
                        }
                        break;

                    case "source": //The feed that the feed item came from.
                        if (feed.CurrentItem != null)
                            feed.CurrentItem.Source = new FeedLink() { Url = new Uri(reader.GetAttribute("url")), Text = await reader.ReadElementContentAsStringAsync() };
                        break;

                    #endregion Feed Item

                    #endregion Optional

                    default: //Unknown feed node, continue to next.
                        result = false;
                        break;
                }
            }
            else if (reader.NodeType == XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "item": //Feed item end, close current feed item.
                        feed.CloseItem();
                        break;
                }
            }

            //Return result
            return result;
        }
    }
}