using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class Rss1Parser : IParser
    {
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new Rss1Parser());

        private Rss1Parser()
        {
        }

        public Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}