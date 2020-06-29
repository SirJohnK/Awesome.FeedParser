using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal sealed class YoutubeParser : BaseParser
    {
        public static string Namespace { get; } = @"http://www.youtube.com/xml/schemas/2015";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new YoutubeParser());

        private YoutubeParser()
        {
        }

        public override Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}