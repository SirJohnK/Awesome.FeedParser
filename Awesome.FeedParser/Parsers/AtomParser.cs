using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal sealed class AtomParser : BaseParser
    {
        public static string Namespace { get; } = @"http://www.w3.org/2005/Atom";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new AtomParser());

        private AtomParser()
        {
        }

        public override Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}