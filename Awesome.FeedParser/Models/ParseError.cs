using Awesome.FeedParser.Extensions;
using System;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Container for parse error information.
    /// </summary>
    public class ParseError
    {
        /// <summary>
        /// Initializes a new ParseError with error information.
        /// </summary>
        /// <param name="node">Current node information.</param>
        /// <param name="parser">Current parser.</param>
        /// <param name="errorType">Current error type.</param>
        /// <param name="parseType">Current parse type.</param>
        /// <param name="parseValue">Current parse value.</param>
        /// <param name="message">Error message.</param>
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

        /// <summary>
        /// Error node information.
        /// </summary>
        public NodeInformation? Node { get; }

        /// <summary>
        /// Parser at the time of the error.
        /// </summary>
        public string? Parser { get; }

        /// <summary>
        /// Parse type at the time of the error.
        /// </summary>
        public ParseType? ParseType { get; }

        /// <summary>
        /// Error parse type.
        /// </summary>
        public ParseErrorType? ParseErrorType { get; }

        /// <summary>
        /// Error parse value.
        /// </summary>
        public string? ParseValue { get; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Get the Parse Type Name.
        /// </summary>
        private string ParseTypeName => Enum.GetName(typeof(ParseType), ParseType);

        /// <summary>
        /// Split Parse Error Type Camel Case and return result
        /// </summary>
        private string ParseErrorTypeName => Enum.GetName(typeof(ParseErrorType), ParseErrorType).SplitCamelCase();

        /// <summary>
        /// Custom ToString to extract error information based on parse error type.
        /// </summary>
        /// <returns>Custom parse error information.</returns>
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

        /// <summary>
        /// Creates a new ParseError instance with error information.
        /// </summary>
        /// <param name="node">Current node information.</param>
        /// <param name="parser">Current parser.</param>
        /// <param name="errorType">Current error type.</param>
        /// <param name="parseType">Current parse type.</param>
        /// <param name="parseValue">Current parse value.</param>
        /// <param name="message">Error message.</param>
        /// <returns>ParseError instance.</returns>
        public static ParseError Create(NodeInformation node, string parser, ParseErrorType errorType, ParseType parseType, string? parseValue = null, string? message = null)
        {
            //Create new ParseError instance
            return new ParseError(node, parser, errorType, parseType, parseValue, message);
        }
    }
}