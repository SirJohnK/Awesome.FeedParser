using Awesome.FeedParser.Exceptions;
using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser
{
    public static class FeedParser
    {
        /// <summary>
        /// Supported Namespace Parsers
        /// </summary>
        private static Dictionary<string, Lazy<IParser>> parsers = new Dictionary<string, Lazy<IParser>>()
        {
            { AtomParser.Namespace, AtomParser.Instance },
            { ContentParser.Namespace, ContentParser.Instance },
            { ITunesParser.Namespace, ITunesParser.Instance },
            { MediaRSSParser.Namespace, MediaRSSParser.Instance },
            { SpotifyParser.Namespace, SpotifyParser.Instance },
            { YoutubeParser.Namespace, YoutubeParser.Instance },
        };

        /// <summary>
        /// Parse root node to determine feed type.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="feed"></param>
        /// <returns>Specific Feed type parser.</returns>
        /// <remarks>
        /// RSS1 = <rss version="1.0">
        /// RSS2 = <rss version="2.0">
        /// Atom = <feed xmlns="http://www.w3.org/2005/Atom">
        /// </remarks>
        private static Lazy<IParser>? ParseFeedType(XmlReader reader, Feed feed)
        {
            //Init
            Lazy<IParser>? feedTypeParser = null;

            //Determine Feed Type
            switch (reader.Name)
            {
                case "rss":
                    //Get RSS version number
                    var version = reader.GetAttribute("version");
                    switch (version)
                    {
                        case "1.0":
                            feed.Type = FeedType.RSS1;
                            feedTypeParser = Rss1Parser.Instance;
                            break;

                        case "2.0":
                            feed.Type = FeedType.RSS2;
                            feedTypeParser = Rss2Parser.Instance;
                            break;
                    }
                    break;

                case "feed":
                    if (string.Equals(reader.GetAttribute("xmlns"), @"http://www.w3.org/2005/Atom"))
                    {
                        feed.Type = FeedType.Atom;
                        feedTypeParser = AtomParser.Instance;
                    }
                    break;
            }

            //Return Feed Type Parser
            return feedTypeParser;
        }

        //private static Task<bool> Parse(string name, XmlReader reader, Feed feed, Lazy<IParser> parser)
        //{
        //    //Feed or Item Node?
        //    return feed.CurrentItem == null ? parser.Value.Parse(name, reader, feed) : parser.Value.Parse(name, reader, feed.CurrentItem);
        //}

        public static async Task<Feed> ParseFeedAsync(string source, Stream stream, CancellationToken cancellationToken)
        {
            //Init
            bool parseNode;
            var feed = new Feed();
            var defaultParser = Rss1Parser.Instance;
            var settings = new XmlReaderSettings() { CloseInput = true, ConformanceLevel = ConformanceLevel.Document, IgnoreWhitespace = true, IgnoreComments = true, Async = true };
            using var reader = XmlReader.Create(stream, settings);

            try
            {
                //Start Reading and move to root node
                if (parseNode = await reader.MoveToContentAsync() != XmlNodeType.None)
                {
                    //Attempt to identify feed type
                    defaultParser = ParseFeedType(reader, feed) ?? defaultParser;

                    //Read first feed node
                    parseNode = await reader.ReadAsync();

                    //Identify and parse nodes until EOF or Cancellation
                    while (parseNode && !reader.EOF && !cancellationToken.IsCancellationRequested)
                    {
                        //Feed node or Extended Namespace node?
                        if (string.IsNullOrWhiteSpace(reader.NamespaceURI))
                            //Parse node with default feed parser
                            parseNode = await defaultParser.Value.Parse(reader, feed);
                        else if (parseNode = parsers.TryGetValue(reader.NamespaceURI, out var parser))
                            //Parse node with current Namespace parser
                            parseNode = await parser.Value.Parse(reader, feed);

                        //Read next node if ignored node or wrong nodetype
                        if (!parseNode) parseNode = await reader.ReadAsync();
                    }
                }
            }
            //Rethrow Feed Parser Exception
            catch (FeedParseException ex) { throw ex; }
            catch (Exception ex)
            {
                //Rethrow all other exceptions as FeedParseException
                throw new FeedParseException(source, reader, ex);
            }
            finally
            {
                //Close Reader and Stream
                reader?.Close();
            }

            //Return Parsed Feed
            return feed;
        }

        public static async Task<Feed> ParseFeedAsync(string url, CancellationToken cancellationToken)
        {
            //Verify parameter
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new ArgumentException("Must be a well formed URL!", nameof(url));

            //Setup Http Client Get Request
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            //Read response stream regardless of status
            var stream = await response.Content.ReadAsStreamAsync();

            //If Success, parse feed
            if (response.IsSuccessStatusCode) return await ParseFeedAsync(url, stream, cancellationToken); ;

            //If Failure, Read Error Content and throw Exception
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            throw new FeedHttpException
            {
                Url = url,
                StatusCode = response.StatusCode,
                Content = content
            };
        }
    }
}