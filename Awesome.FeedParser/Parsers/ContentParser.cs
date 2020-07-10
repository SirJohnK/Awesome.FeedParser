using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal sealed class ContentParser : BaseParser
    {
        //Content Namespace URI
        public static string Namespace { get; } = @"http://purl.org/rss/1.0/modules/content/";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new ContentParser());

        private ContentParser()
        {
        }

        public override Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true)
        {
            return Task.FromResult(false);
        }
    }
}