using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal sealed class RSS_1_0_Parser : BaseParser
    {
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RSS_1_0_Parser());

        private RSS_1_0_Parser()
        {
        }

        public override Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}