namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Media status state.
    /// </summary>
    public enum MediaStatusState
    {
        Active = 0,     //Means a media object is active in the system.
        Blocked = 1,    //Means a media object is blocked by the publisher.
        Deleted = 2     //Means a media object has been deleted by the publisher.
    }
}