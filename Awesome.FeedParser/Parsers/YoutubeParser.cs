using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using Awesome.FeedParser.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal sealed class YoutubeParser : BaseParser
    {
        public static string Namespace { get; } = @"http://www.youtube.com/xml/schemas/2015";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new YoutubeParser());

        private YoutubeParser()
        {
        }

        public override Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true)
        {
            return Task.FromResult(false);
        }
    }
}