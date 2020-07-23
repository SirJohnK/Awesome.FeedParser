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
                NodeInformation nodeInfo = reader.NodeInformation();

                //Add Media RSS to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.MediaRSS;

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Common

                    case "backLinks": //Allows inclusion of all the URLs pointing to a media object.
                        {
                            break;
                        }

                    case "category": //Allows tags or categories to be set for the media.
                        {
                            break;
                        }

                    case "credit": //Allows credit to be given to people and entities for the creation of the media.
                        {
                            break;
                        }

                    case "comments": //Allows inclusion of all the comments a media object has received.
                        {
                            break;
                        }

                    case "community": //Allows inclusion of the user perception about a media object in the form of view count, ratings and tags.
                        {
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
                            break;
                        }

                    case "hash": //A "md5" or "sha-1" hash of the media can be used to help verify the integrity and/or look for duplicates.
                        {
                            break;
                        }

                    case "keywords": //A short list or comma separated words and phrases describing the media content.
                        {
                            break;
                        }

                    case "license": //Specify the machine-readable license associated with the content.
                        {
                            break;
                        }

                    case "location": //Geographical information about various locations captured in the content of a media object. (Conforms to geoRSS)
                        {
                            break;
                        }

                    case "peerLink": //P2P link.
                        {
                            break;
                        }

                    case "player": //Required if <content> did not specify the "url" attribute. This tag allows the media object to be accessed through a web browser or media console.
                        {
                            break;
                        }

                    case "price": //Pricing information about a media object.
                        {
                            break;
                        }

                    case "rating": //Allows the permissible audience to be declared.
                        {
                            break;
                        }

                    case "responses": //Allows inclusion of a list of all media responses a media object has received.
                        {
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
                            break;
                        }

                    case "scenes": //Specifies various scenes within a media object.
                        {
                            break;
                        }

                    case "subTitle": //Subtitle/CC link.
                        {
                            break;
                        }

                    case "text": //Allows text to be included with the media object.
                        {
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
                            //Get content attributes
                            while (reader.MoveToNextAttribute())
                            {
                                //Attempt to parse attribute
                                switch (reader.LocalName)
                                {
                                    case "bitrate": //Kilobits per second rate of media.
                                        {
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
                                            break;
                                        }
                                    case "fileSize": //Number of bytes of the media object.
                                        {
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
                                            break;
                                        }
                                    case "url": //Specify the direct URL to the media object.
                                        {
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
                            break;
                        }

                    case "group": //Allows for grouping of item media representations.
                        {
                            //Add new media
                            if (feed.CurrentParseType == ParseType.Item) feed.CurrentItem?.AddMedia();
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
                    case "group": //Media group end, close current media.
                        {
                            feed.CurrentItem?.CloseMedia();
                            break;
                        }
                }
            }

            //Return result
            return result;
        }
    }
}