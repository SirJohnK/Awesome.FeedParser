using System;

namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Supported feed content types.
    /// </summary>
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
        GeoRSS = 128,
        DublinCore = 256,
    }
}