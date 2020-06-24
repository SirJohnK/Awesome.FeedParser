using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class ITunesParser : IParser
    {
        public static string Namespace { get; } = @"http://www.itunes.com/dtds/podcast-1.0.dtd";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new ITunesParser());

        private ITunesParser()
        {
        }

        public async Task<bool> Parse(XmlReader reader, Feed feed)
        {
            //Init
            bool result;

            //Verify Element Node
            if (result = reader.NodeType == XmlNodeType.Element)
            {
                //Init
                ICommonITunes target;

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

                    case "author":
                        target.Author = await reader.ReadElementContentAsStringAsync();
                        break;

                    case "block":
                        if (bool.TryParse(await reader.ReadElementContentAsStringAsync(), out var blockFlag))
                            target.Block = blockFlag;
                        break;

                    case "explicit":
                        if (bool.TryParse(await reader.ReadElementContentAsStringAsync(), out var explicitFlag))
                            target.Explicit = explicitFlag;
                        break;

                    case "image": //The artwork for the show/episode.
                        var href = reader.GetAttribute("href");
                        if (href != null) target.Image = new FeedImage() { Url = new Uri(href) };
                        result = false;
                        break;

                    #endregion Common

                    default: //Unknown feed/item node, continue to next.
                        result = false;
                        break;
                }
            }

            //Return result
            return result;
        }
    }
}