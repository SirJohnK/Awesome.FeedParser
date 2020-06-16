using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class ContentParser : IParser
    {
        public static string Namespace { get; } = @"http://purl.org/rss/1.0/modules/content/";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new ContentParser());

        private ContentParser()
        {
        }

        public Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}