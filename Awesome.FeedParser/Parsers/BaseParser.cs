using Awesome.FeedParser.Interfaces;
using Awesome.FeedParser.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Parsers
{
    internal abstract class BaseParser : IParser
    {
        public abstract Task<bool> Parse(XmlReader reader, Feed feed);

        protected void SetParseError(ParseErrorType errorType, NodeInformation nodeInformation, Feed feed, string? content = null, string? message = null)
        {
            var error = ParseError.Create(nodeInformation, ToString(), errorType, feed.CurrentParseType, content, message);
            (feed.ParseError ??= new List<ParseError>()).Add(error);
            Debug.WriteLine(error);
        }
    }
}