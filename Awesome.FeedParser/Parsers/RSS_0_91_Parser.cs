using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal sealed class RSS_0_91_Parser : BaseParser
    {
        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new RSS_0_91_Parser());

        private RSS_0_91_Parser()
        {
        }

        public override Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}