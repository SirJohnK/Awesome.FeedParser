namespace Awesome.FeedParser.Interfaces.Media
{
    /// <summary>
    /// Interface to access Media hash.
    /// </summary>
    public interface IMediaHash
    {
        /// <summary>
        /// Algorithm used to create the hash.
        /// </summary>
        /// <remarks>
        /// Possible values are "md5" and "sha-1".
        /// </remarks>
        public string? Algo { get; }

        /// <summary>
        /// Hash value.
        /// </summary>
        public string? Hash { get; }
    }
}