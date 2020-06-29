using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Spotify feed nodes.
    /// </summary>
    internal sealed class SpotifyParser : BaseParser
    {
        //Spotify Namespace URI
        public static string Namespace { get; } = @"https://www.spotify.com/ns/rss";

        //Parser lazy loaded instance
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new SpotifyParser());

        //Private constructor to prevent external initalization
        private SpotifyParser()
        {
        }

        /// <summary>
        /// Main Spotify parsing method.
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
                NodeInformation nodeInfo = reader.NodeInformation();
                feed.Spotify ??= new SpotifyFeed();

                //Add Spotify to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.Spotify;

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Feed

                    case "limit": //Number of concurrent episodes (items) to display.
                        {
                            if (feed.Spotify != null)
                            {
                                //Get limit count
                                var content = reader.GetAttribute("recentCount");
                                if (content != null)
                                {
                                    if (int.TryParse(content, out var count))
                                        feed.Spotify.Limit = count;
                                    else
                                        //Unknown node format
                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                                }
                                else
                                {
                                    //Missing count attribute
                                    SetParseError(ParseErrorType.MissingAttribute, nodeInfo, feed, null, "recentCount");
                                }
                            }
                            result = false;
                        }
                        break;

                    case "countryOfOrigin": //Defines the intended market/territory ranked in order of priority where the podcast is relevant to the consumer. (List of ISO 3166 codes).
                        {
                            if (feed.Spotify != null)
                            {
                                //Get Countries of origin
                                var content = await reader.ReadElementContentAsStringAsync();
                                try
                                {
                                    //Attempt to set country of origin list
                                    feed.Spotify.CountryOfOrigin = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(country => new RegionInfo(country));
                                }
                                catch (ArgumentException ex)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, ex.Message);
                                }
                            }
                            else
                                result = false;
                            break;
                        }

                    #endregion Feed

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