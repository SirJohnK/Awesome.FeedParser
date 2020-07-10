namespace Awesome.FeedParser.Models
{
    public enum ParseErrorType
    {
        General = 0,
        MissingAttribute = 1,
        MissingNode = 2,
        UnknownNamespace = 3,
        UnknownNode = 4,
        UnknownNodeFormat = 5,
        UnknownSubNode = 6,
    }
}