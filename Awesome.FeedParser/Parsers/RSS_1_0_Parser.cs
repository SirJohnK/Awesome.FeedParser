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
    /// Parser for RSS 1.0 feed nodes.
    /// </summary>
    internal class RSS_1_0_Parser : RSS_0_92_Parser
    {
        //RSS 1.0 Namespace URI
        public static string Namepace => @"http://purl.org/rss/1.0/";

        //Parser lazy loaded instance
        public new static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RSS_1_0_Parser());

        //Protected constructor to prevent external initalization
        protected RSS_1_0_Parser()
        {
        }

        /// <summary>
        /// Main RSS 1.0 parsing method.
        /// </summary>
        /// <param name="parent">Parent stack for current node.</param>
        /// <param name="reader">Current xml feed reader.</param>
        /// <param name="feed">Current feed result.</param>
        /// <param name="root">Flag indicating if parser is the default root parser.</param>
        /// <returns>Flag indicating if current node should be parsed or if next node should be retrieved.</returns>
        /// <remarks>
        /// Because RSS 1.0 almost only overrides older RSS versions, if feed type is NOT RSS 1.0, parsing is handled by base classes.
        /// </remarks>
        public override async Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true)
        {
            //Init
            bool result;

            //Verify Element Node and feed type
            if (result = reader.NodeType == XmlNodeType.Element && (!reader.IsEmptyElement || reader.HasAttributes) && feed.Type == FeedType.RSS_1_0)
            {
                //Init
                var nodeInfo = reader.NodeInformation();

                //Identify node name
                switch (reader.LocalName)
                {
                    #region Required

                    #region Feed

                    case "channel": //Feed root node. (Override RSS 0.91)
                        {
                            //Attempt to get about attribute
                            var about = reader.GetAttribute("rdf:about");
                            if (about != null)
                            {
                                try
                                {
                                    //Attempt to parse about URL
                                    feed.About = new Uri(about);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, about, $"Node: rdf:about, {ex.Message}");
                                }
                            }
                            else
                            {
                                //Missing about attribute
                                SetParseError(ParseErrorType.MissingAttribute, nodeInfo, feed, null, "rdf:about");
                            }
                            break;
                        }

                    #endregion Feed

                    #region Item

                    case "item": //Feed item start, add new feed item to feed. (Override RSS 0.91)
                        {
                            // Init
                            var item = feed.CurrentItem ?? feed.AddItem();

                            //Attempt to get about attribute
                            var about = reader.GetAttribute("rdf:about");
                            if (about != null)
                            {
                                try
                                {
                                    //Attempt to parse about URL
                                    item.About = new Uri(about);
                                }
                                catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                {
                                    //Unknown node format
                                    SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, about, $"Node: rdf:about, {ex.Message}");
                                }
                            }
                            else
                            {
                                //Missing about attribute
                                SetParseError(ParseErrorType.MissingAttribute, nodeInfo, feed, null, "rdf:about");
                            }
                            break;
                        }

                    #endregion Item

                    #endregion Required

                    #region Optional

                    #region Feed

                    case "image": //Specifies a GIF, JPEG or PNG image that can be displayed with the feed. (Override RSS 0.91)
                        {
                            //Verify Correct Image Node. If empty, ignore. Otherwise parse.
                            if (!reader.IsEmptyElement)
                            {
                                //Init
                                var image = feed.Image ?? new FeedImage();

                                //Attempt to get about attribute
                                var about = reader.GetAttribute("rdf:about");
                                if (about != null)
                                {
                                    try
                                    {
                                        //Attempt to parse about URL
                                        image.About = new Uri(about);
                                    }
                                    catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                    {
                                        //Unknown node format
                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, about, $"Node: rdf:about, {ex.Message}");
                                    }
                                }
                                else
                                {
                                    //Missing about attribute
                                    SetParseError(ParseErrorType.MissingAttribute, nodeInfo, feed, null, "rdf:about");
                                }

                                //Get image properties
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
                                                SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, element.Value, element.Key);
                                                break;
                                            }
                                    }
                                }
                                feed.Image = image;
                            }
                            break;
                        }

                    case "items": break; //An RDF Sequence is used to contain all the items to denote item order for rendering and reconstruction. (Ignore)

                    case "textinput": //Specifies a text input box that can be displayed with the feed. (Override RSS 0.91)
                        {
                            //Verify Correct text input Node. If empty, ignore. Otherwise parse.
                            if (!reader.IsEmptyElement)
                            {
                                //Init
                                var textInput = feed.TextInput ?? new FeedTextInput();

                                //Attempt to get about attribute
                                var about = reader.GetAttribute("rdf:about");
                                if (about != null)
                                {
                                    try
                                    {
                                        //Attempt to parse about URL
                                        textInput.About = new Uri(about);
                                    }
                                    catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                    {
                                        //Unknown node format
                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, about, $"Node: rdf:about, {ex.Message}");
                                    }
                                }
                                else
                                {
                                    //Missing about attribute
                                    SetParseError(ParseErrorType.MissingAttribute, nodeInfo, feed, null, "rdf:about");
                                }

                                //Get text input properties
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
                                        default: SetParseError(ParseErrorType.UnknownSubNode, nodeInfo, feed, element.Value, element.Key); break;
                                    }
                                }
                                feed.TextInput = textInput;
                            }
                            break;
                        }

                    #endregion Feed

                    #endregion Optional

                    default: //Unknown feed/item node
                        {
                            //Try RSS 0.92 Parse
                            result = await base.Parse(parent, reader, feed, false);
                            if (!result && root) SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            break;
                        }
                }
            }
            else
            {
                //Try RSS 0.92 Parse
                result = await base.Parse(parent, reader, feed, false);
            }

            //Return result
            return result;
        }
    }
}