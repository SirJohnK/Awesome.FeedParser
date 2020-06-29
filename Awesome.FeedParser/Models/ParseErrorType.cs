namespace Awesome.FeedParser.Models
{
    public enum ParseErrorType
    {
        General = 0,
        UnknownNode = 1,
        UnknownNodeFormat = 2,
        UnknownNamespace = 3,
        MissingNode = 4,
        MissingAttribute = 5
    }
}