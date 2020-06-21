using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class RSS_1_0_Parser : IParser
    {
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RSS_1_0_Parser());

        private RSS_1_0_Parser()
        {
        }

        public Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}