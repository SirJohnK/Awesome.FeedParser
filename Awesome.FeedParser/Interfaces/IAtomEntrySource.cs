using System;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access Atom entry source properties.
    /// </summary>
    public interface IAtomEntrySource
    {
        /// <summary>
        /// Identifies the source feed using a universally unique and permanent URI.
        /// </summary>
        public Uri? Id { get; }

        /// <summary>
        /// The name of the source feed.
        /// </summary>
        public string? Title { get; }

        /// <summary>
        /// Indicates the last time the source feed was modified in a significant way.
        /// </summary>
        public DateTime? Updated { get; }
    }
}