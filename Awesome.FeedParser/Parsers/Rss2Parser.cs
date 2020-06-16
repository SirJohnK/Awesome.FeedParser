using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class Rss2Parser : IParser
    {
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new Rss2Parser());

        private Rss2Parser()
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
                    case "title":
                        target.Title = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "link":
                        target.Link = new Uri(await reader.ReadElementContentAsStringAsync());
                        break;

                    case "description":
                        target.Description = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "pubDate":
                        if (DateTime.TryParse(await reader.ReadElementContentAsStringAsync(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var pubDate))
                            target.PubDate = pubDate;
                        break;

                    case "image":
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

                    case "language":
                        feed.Language = CultureInfo.GetCultureInfo(await reader.ReadElementContentAsStringAsync());
                        break;

                    case "copyright":
                        feed.Copyright = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "item":
                        feed.AddItem();
                        result = false;
                        break;

                    default:
                        result = false;
                        break;
                }
            }
            else if (reader.NodeType == XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "item":
                        feed.CloseItem();
                        break;
                }
            }

            //Return result
            return result;
        }
    }
}