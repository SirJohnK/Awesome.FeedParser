using Awesome.FeedParser.Extensions;
using Awesome.FeedParser.Models;
using System;
using System.Xml;

namespace Awesome.FeedParser.Exceptions
{
    /// <summary>
    /// Feed Parser Parse Exception.
    /// </summary>
    public class FeedParseException : Exception
    {
        /// <summary>
        /// Main feed parser exception.
        /// </summary>
        /// <param name="source">Feed source.</param>
        /// <param name="reader">Xml reader for curret feed.</param>
        /// <param name="innerexception">Catched feed parser exception.</param>
        public FeedParseException(string source, XmlReader reader, Exception innerexception) : base(innerexception.Message, innerexception)
        {
            //Init
            Source = source;
            Node = reader.NodeInformation();
        }

        /// <summary>
        /// Current node information.
        /// </summary>
        public NodeInformation Node { get; }
    }
}