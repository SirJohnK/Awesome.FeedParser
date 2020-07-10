using Awesome.FeedParser.Utils;
using System;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ParseError
    {
        public ParseError(NodeInformation node, string parser, ParseErrorType errorType, ParseType parseType, string? parseValue = null, string? message = null)
        {
            //Init
            Node = node;
            Parser = parser;
            ParseErrorType = errorType;
            ParseType = parseType;
            ParseValue = parseValue;
            Message = message;
        }

        //Node Information
        public NodeInformation? Node { get; }

        public string? Parser { get; }
        public ParseType? ParseType { get; }
        public ParseErrorType? ParseErrorType { get; }
        public string? ParseValue { get; }
        public string? Message { get; }

        //Get Parse Type Name
        private string ParseTypeName => Enum.GetName(typeof(ParseType), ParseType);

        //Split Parse Error Type Camel Case and return result
        private string ParseErrorTypeName => Enum.GetName(typeof(ParseErrorType), ParseErrorType).SplitCamelCase();

        public override string ToString()
        {
            //Init
            var message = $"({Parser}) {ParseErrorTypeName} ({ParseTypeName}): {Node?.Name}";

            //Add type specific information
            message += ParseErrorType switch
            {
                Models.ParseErrorType.MissingAttribute => $", Attribute: {Message}",
                Models.ParseErrorType.MissingNode => $", Node: {Message}",
                Models.ParseErrorType.UnknownNamespace => $", Namespace: {Node?.Namespace}",
                Models.ParseErrorType.UnknownNode => string.Empty,
                Models.ParseErrorType.UnknownNodeFormat => $", Content: {ParseValue}, Message: {Message}",
                Models.ParseErrorType.UnknownSubNode => $", Subnode: {Message}, Content: {ParseValue}",
                _ => string.Empty
            } + $", Line: {Node?.LineNumber}, Pos: {Node?.LinePosition}";

            //Return error message
            return message;
        }

        public static ParseError Create(NodeInformation node, string parser, ParseErrorType errorType, ParseType parseType, string? parseValue = null, string? message = null)
        {
            //Create new ParseError instance
            return new ParseError(node, parser, errorType, parseType, parseValue, message);
        }
    }
}