namespace Awesome.FeedParser.Interfaces.Common
{
    /// <summary>
    /// Interface to access common feed/item category properties.
    /// </summary>
    public interface ICommonFeedCategory
    {
        #region Required

        /// <summary>
        /// Identifies a hierarchic location in the indicated taxonomy.
        /// </summary>
        public string? Category { get; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Identifies a categorization taxonomy.
        /// </summary>
        public string? Domain { get; }

        #endregion Optional
    }
}