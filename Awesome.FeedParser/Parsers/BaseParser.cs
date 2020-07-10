using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    /// <summary>
    /// Parser base class.
    /// </summary>
    internal abstract class BaseParser : IParser
    {
        /// <summary>
        /// Abstract implementation of IParser method.
        /// </summary>
        /// <param name="parent">Parent stack for current node.</param>
        /// <param name="reader">Current xml feed reader.</param>
        /// <param name="feed">Current feed result.</param>
        /// <param name="root">Flag indicating if parser is the default root parser.</param>
        /// <returns>Flag indicating if current node should be parsed or if next node should be retrieved.</returns>
        public abstract Task<bool> Parse(Stack<NodeInformation> parent, XmlReader reader, Feed feed, bool root = true);

        /// <summary>
        /// Create and Add Parse error to the current feed result.
        /// </summary>
        /// <param name="errorType">Type of parse error.</param>
        /// <param name="nodeInformation">Current node base information.</param>
        /// <param name="feed">Current feed result.</param>
        /// <param name="content">Current node content data. (Optonal)</param>
        /// <param name="message">Parse error message. (Optional)</param>
        protected void SetParseError(ParseErrorType errorType, NodeInformation nodeInformation, Feed feed, string? content = null, string? message = null)
        {
            //Create and Add Parse error to the current feed result
            var error = ParseError.Create(nodeInformation, ToString(), errorType, feed.CurrentParseType, content, message);
            (feed.ParseError ??= new List<ParseError>()).Add(error);
            Debug.WriteLine(error);
        }
    }
}