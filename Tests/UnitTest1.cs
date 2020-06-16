using Awesome.FeedParser;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //Init
            var feed = await FeedParser.ParseFeedAsync(@"https://feed.khz.se/nordigt", CancellationToken.None);

            //Assert
            feed.Should().NotBeNull();
        }
    }
}