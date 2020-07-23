using System;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Feed Media enclosure.
    /// </summary>
    public interface IEnclosure
    {
        public Uri? Url { get; }
        public long? Length { get; }
        public string? Type { get; }
    }
}