using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class AtomParser : IParser
    {
        public static string Namespace { get; } = @"http://www.w3.org/2005/Atom";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new AtomParser());

        private AtomParser()
        {
        }

        public Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}