using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal sealed class MediaRSSParser : BaseParser
    {
        //MediaRSS Namespace URI
        public static string Namespace { get; } = @"http://search.yahoo.com/mrss/";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new MediaRSSParser());

        private MediaRSSParser()
        {
        }

        public override Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true)
        {
            return Task.FromResult(false);
        }
    }
}