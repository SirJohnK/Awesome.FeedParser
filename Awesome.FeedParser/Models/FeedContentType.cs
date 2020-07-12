using System;

namespace Awesome.FeedParser.Models
{
    [Flags]
    public enum FeedContentType
    {
        Basic = 0,
        Atom = 1,
        Rdf = 2,
        Content = 4,
        ITunes = 8,
        MediaRSS = 16,
        Spotify = 32,
        Youtube = 64,
    }
}