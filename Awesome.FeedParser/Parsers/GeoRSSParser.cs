using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Geo RSS Simple and GML feed nodes.
    /// </summary>
    internal sealed class GeoRSSParser : BaseParser
    {
        //Geo RSS Namespace URI:s
        public static string Namespace => @"http://www.georss.org/georss";

        public static string SecondNamespace => @"http://www.opengis.net/gml";

        //Parser lazy loaded instance
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new GeoRSSParser());

        //Private constructor to prevent external initalization
        private GeoRSSParser()
        {
        }

        /// <summary>
        /// Parse and set coordinates.
        /// </summary>
        /// <param name="type">Geographical information type.</param>
        /// <param name="target">Target geographical information.</param>
        /// <param name="reader">Current xml feed reader.</param>
        /// <param name="feed">Current feed.</param>
        /// <param name="nodeInfo">Current node information.</param>
        /// <returns>List of geographical coordinates.</returns>
        private async Task SetCoordinates(GeoType type, GeoInformation target, XmlReader reader, Feed feed, NodeInformation nodeInfo)
        {
            //Init
            target.coordinates ??= new List<GeoCoordinate>();

            if (!reader.IsEmptyElement)
            {
                var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    //Attempt to parse coordinates
                    var index = 0;
                    content = content.Replace(",", ".").Trim();
                    var coords = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(part => part.Trim()).ToList();
                    if (coords.Count % 2 == 0)
                    {
                        //Set target type
                        target.Type = type;

                        //Get all coordinates
                        while (index < coords.Count)
                        {
                            if (double.TryParse(coords[index], NumberStyles.Float, CultureInfo.InvariantCulture, out var latitude))
                            {
                                if (double.TryParse(coords[index + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out var longitude))
                                {
                                    try
                                    {
                                        //Add coordinate to target coordinates
                                        target.coordinates.Add(new GeoCoordinate(latitude, longitude));
                                    }
                                    catch (ArgumentOutOfRangeException ex)
                                    {
                                        //Unknown coordinates format
                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, $"Latitude: {latitude}, Longitude: {longitude}", ex.Message);
                                    }

                                    //Next coordinates
                                    index += 2;
                                }
                                else
                                {
                                    //Parse longitude failed!
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, coords[index + 1], $"Parse longitude failed! (Index: {index + 1})");
                                }
                            }
                            else
                            {
                                //Parse latitude failed!
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, coords[index], $"Parse latitude failed! (Index: {index})");
                            }
                        }
                    }
                    else
                    {
                        //Coordinates must have both latitude and longitude!
                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content, "Coordinates must have both latitude and longitude!");
                    }
                }
            }
        }

        /// <summary>
        /// Main Geo RSS parsing method.
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
                GeoInformation? target = null;
                var nodeInfo = reader.NodeInformation();

                //Add Geo RSS to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.GeoRSS;

                //Media RSS Parent?
                var mediaParent = parent.FirstOrDefault(ancestor => ancestor.Namespace == FeedParser.ContentTypeNamespace[FeedContentType.MediaRSS]);
                if (mediaParent != null)
                {
                    switch (mediaParent.LocalName)
                    {
                        case "location":
                            {
                                if (feed.CurrentItem != null)
                                    target = feed.CurrentItem.Media?.CurrentInformation?.Locations?.LastOrDefault()?.GeoInformation ?? new GeoInformation();
                                else
                                    target = feed.MediaInformation?.Locations?.LastOrDefault()?.GeoInformation ?? new GeoInformation();
                                break;
                            }
                    }
                }

                //Set feed/feed item target
                if (target == null)
                {
                    if (feed.CurrentItem != null)
                    {
                        //Get/Set feed item geographical information
                        target = feed.CurrentItem.GeoInformation ??= new GeoInformation();
                    }
                    else
                    {
                        //Get/Set feed geographical information
                        target = feed.GeoInformation ??= new GeoInformation();
                    }
                }

                //Identify node name
                switch (reader.LocalName)
                {
                    case "box": //A bounding box is a rectangular region, often used to define the extents of a map or a rough area of interest.
                    case "lowerCorner":
                    case "upperCorner":
                        {
                            //Set target box coordinates
                            await SetCoordinates(GeoType.Box, target, reader, feed, nodeInfo).ConfigureAwait(false);
                            break;
                        }

                    case "elev":
                        {
                            //Init
                            var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);

                            //Attempt to parse elevation.
                            if (int.TryParse(content, out var elevation))
                            {
                                target.Elevation = elevation;
                            }
                            else
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            }
                            break;
                        }

                    case "featurename": //Feauture name of the geographical information.
                        {
                            //Attempt to parse feature name.
                            target.FeatureName = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);
                            break;
                        }

                    case "featuretypetag": //Feauture type of the geographical information.
                        {
                            //Attempt to parse feature type.
                            target.FeatureType = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);
                            break;
                        }

                    case "floor":
                        {
                            //Init
                            var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);

                            //Attempt to parse floor.
                            if (int.TryParse(content, out var floor))
                            {
                                target.Floor = floor;
                            }
                            else
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            }
                            break;
                        }

                    case "line": //A line contains a space seperated list of latitude-longitude pairs, with each pair separated by whitespace.
                        {
                            //Set target line coordinates
                            await SetCoordinates(GeoType.Line, target, reader, feed, nodeInfo).ConfigureAwait(false);
                            break;
                        }

                    case "point": //A point contains a single latitude-longitude pair, separated by whitespace.
                    case "pos":
                        {
                            //Set target point coordinates
                            await SetCoordinates(GeoType.Point, target, reader, feed, nodeInfo).ConfigureAwait(false);
                            break;
                        }

                    case "posList":
                        {
                            switch (parent.Peek().LocalName)
                            {
                                case "LinearRing": //A polygon contains a space seperated list of latitude-longitude pairs, with each pair separated by whitespace.
                                    {
                                        //Set target polygon coordinates
                                        await SetCoordinates(GeoType.Polygon, target, reader, feed, nodeInfo).ConfigureAwait(false);
                                        break;
                                    }
                                case "LineString": //A line contains a space seperated list of latitude-longitude pairs, with each pair separated by whitespace.
                                    {
                                        //Set target line coordinates
                                        await SetCoordinates(GeoType.Line, target, reader, feed, nodeInfo).ConfigureAwait(false);
                                        break;
                                    }
                            }
                            break;
                        }

                    case "polygon": //A polygon contains a space seperated list of latitude-longitude pairs, with each pair separated by whitespace.
                        {
                            //Set target polygon coordinates
                            await SetCoordinates(GeoType.Polygon, target, reader, feed, nodeInfo).ConfigureAwait(false);
                            break;
                        }

                    case "radius":
                        {
                            //Init
                            var content = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);

                            //Attempt to parse radius.
                            if (int.TryParse(content, out var radius))
                            {
                                target.Radius = radius;
                            }
                            else
                            {
                                //Unknown node format
                                SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, content);
                            }
                            break;
                        }

                    case "relationshiptag": //Relationship of the geographical information.
                        {
                            //Attempt to parse relationship.
                            target.Relationship = await reader.ReadStartElementAndContentAsStringAsync().ConfigureAwait(false);
                            break;
                        }

                    case "Envelope":
                    case "exterior":
                    case "LinearRing":
                    case "LineString":
                    case "Point":
                    case "where": break;

                    default: //Unknown feed/item node, continue to next.
                        {
                            result = false;
                            if (root) SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            break;
                        }
                }
            }

            //Return result
            return result;
        }
    }
}