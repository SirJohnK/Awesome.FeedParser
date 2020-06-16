using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    public sealed class SpotifyParser : IParser
    {
        public static string Namespace { get; } = @"https://www.spotify.com/ns/rss";

        public static Lazy<IParser> Instance { get; } = new Lazy<IParser>(() => new SpotifyParser());

        private SpotifyParser()
        {
        }

        public Task<bool> Parse(XmlReader reader, Feed feed)
        {
            return Task.FromResult(false);
        }
    }
}