using Awesome.FeedParser.Models.Common;

namespace Awesome.FeedParser.Models.DublinCore
{
    /// <summary>
    /// Dublin Core Parser Feed Result Class.
    /// </summary>
    public class DCMetaData
    {
        /// <summary>
        /// A person, an organization, or a service. Typically, the name of a Publisher should be used to indicate the entity.
        /// </summary>
        public FeedText? Publisher { get; internal set; }
    }
}