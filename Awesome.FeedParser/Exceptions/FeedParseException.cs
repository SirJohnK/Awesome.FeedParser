using System;
using System.Xml;

namespace Awesome.FeedParser.Exceptions
{
    /// <summary>
    /// Feed Parser Parse Exception.
    /// </summary>
    public class FeedParseException : Exception
    {
        public FeedParseException(string source, XmlReader reader, Exception innerexception) : base(innerexception.Message, innerexception)
        {
            //Init
            Source = source;
            var lineInfo = (IXmlLineInfo)reader;
            if (lineInfo.HasLineInfo())
            {
                Line = lineInfo.LineNumber;
                Position = lineInfo.LinePosition;
            }
        }

        public int Line { get; }

        public int Position { get; }
    }
}