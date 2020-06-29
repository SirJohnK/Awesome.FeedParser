using System;
using System.Text.RegularExpressions;

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
        private string ParseErrorTypeName => string.Join(" ", Regex.Split(Enum.GetName(typeof(ParseErrorType), ParseErrorType), @"(?<!^)(?=[A-Z])"));

        public override string ToString()
        {
            var message = $"({Parser}) {ParseErrorTypeName} ({ParseTypeName}): {Node?.Name}, Line: {Node?.LineNumber}, Pos: {Node?.LinePosition}";
            return message;
        }

        public static ParseError Create(NodeInformation node, string parser, ParseErrorType errorType, ParseType parseType, string? parseValue = null, string? message = null)
        {
            //Create new ParseError instance
            return new ParseError(node, parser, errorType, parseType, parseValue, message);
        }
    }
}