namespace Awesome.FeedParser.Models.ITunes
{
    /// <summary>
    /// The type of show enum.
    /// </summary>
    public enum ITunesType
    {
        Episodic = 0,   //(default). Specifies thatepisodes are intended to be consumed without any specific order.
        Serial = 1      //Specifies thatepisodes are intended to be consumed in sequential order.
    }
}