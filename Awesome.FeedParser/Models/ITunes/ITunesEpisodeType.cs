namespace Awesome.FeedParser.Models.ITunes
{
    /// <summary>
    /// ITunes episode type enum.
    /// </summary>
    public enum ITunesEpisodeType
    {
        Full = 0,       //(default). Specifies the complete content of show.
        Trailer = 1,    //Specifies a short, promotional piece of content that represents a preview of show.
        Bonus = 2       //Specifies extra content for show (for example, behind the scenes information or interviews with the cast) or cross-promotional content for another show.
    }
}