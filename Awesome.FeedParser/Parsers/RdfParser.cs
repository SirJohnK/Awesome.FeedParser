using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Interfaces.Common;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser for Rdf feed nodes.
    /// </summary>
    internal sealed class RdfParser : BaseParser
    {
        /// <summary>
        /// Rdf Namespace URI:s.
        /// </summary>
        public static IEnumerable<string> Namespaces { get; } = new List<string>()
        {
            { @"http://www.w3.org/1999/02/22-rdf-syntax-ns#" },
        };

        /// <summary>
        /// Parser lazy loaded instance.
        /// </summary>
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RdfParser());

        /// <summary>
        /// Private constructor to prevent external initalization.
        /// </summary>
        private RdfParser()
        {
        }

        /// <summary>
        /// Main Rdf parsing method.
        /// </summary>
        /// <param name="parent">Parent stack for current node.</param>
        /// <param name="reader">Current xml feed reader.</param>
        /// <param name="feed">Current feed result.</param>
        /// <param name="root">Flag indicating if parser is the default root parser.</param>
        /// <returns>Flag indicating if current node should be parsed or if next node should be retrieved.</returns>
        public override Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true)
        {
            //Init
            bool result;

            //Verify Element Node
            if (result = reader.NodeType == XmlNodeType.Element && (!reader.IsEmptyElement || reader.HasAttributes))
            {
                //Init
                var nodeInfo = reader.NodeInformation();
                var parentName = parent.Count > 0 ? parent.Peek().LocalName : string.Empty;

                //Add Rdf to feed content type.
                ICommonFeed feedTarget = feed.CurrentItem ?? (ICommonFeed)feed;
                feedTarget.ContentType |= FeedContentType.Rdf;

                //Identify node name
                switch (reader.LocalName)
                {
                    case "Seq":
                        {
                            switch (parentName)
                            {
                                case "items": //An RDF Sequence is used to contain all the items to denote item order for rendering and reconstruction.
                                    {
                                        var itemsSequence = new List<Uri>();
                                        if (!reader.IsEmptyElement)
                                        {
                                            var subTree = reader.ReadSubtree();
                                            while (subTree.ReadToFollowing("li", reader.NamespaceURI))
                                            {
                                                var resource = subTree.GetAttribute("resource");
                                                if (resource != null)
                                                {
                                                    try
                                                    {
                                                        //Attempt to parse resource URL
                                                        itemsSequence.Add(new Uri(resource));
                                                    }
                                                    catch (Exception ex) when (ex is ArgumentNullException || ex is UriFormatException)
                                                    {
                                                        //Unknown node format
                                                        SetParseError(ParseErrorType.UnknownNodeFormat, nodeInfo, feed, resource, ex.Message);
                                                    }
                                                }
                                                else
                                                {
                                                    //Missing resource attribute
                                                    SetParseError(ParseErrorType.MissingAttribute, nodeInfo, feed, null, "resource");
                                                }
                                            }
                                        }
                                        feed.itemsSequence = itemsSequence;
                                        break;
                                    }

                                default:
                                    result = false;
                                    break;
                            }
                            break;
                        }

                    default: //Unknown feed/item node, continue to next.
                        {
                            result = false;
                            if (root) SetParseError(ParseErrorType.UnknownNode, nodeInfo, feed);
                            break;
                        }
                }
            }

            //Return result
            return Task.FromResult(result);
        }
    }
}