using System;

namespace Awesome.FeedParser.Interfaces.Common
{
    /// <summary>
    /// Interface to access common item enclosure properties.
    /// </summary>
    public interface ICommonItemEnclosure
    {
        #region Required

        /// <summary>
        /// Url where the enclosure is located.
        /// </summary>
        public Uri? Url { get; }

        /// <summary>
        /// Length how big the enclosure is in bytes.
        /// </summary>
        public long? Length { get; }

        /// <summary>
        /// Type what the enclosure type is, a standard MIME type.
        /// </summary>
        public string? Type { get; }

        #endregion Required
    }
}