using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class ITunesParser : IParser
    {
        public static string Namespace { get; } = @"http://www.itunes.com/dtds/podcast-1.0.dtd";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new ITunesParser());

        private ITunesParser()
        {
        }

        public Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}