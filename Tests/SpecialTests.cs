using Awesome.FeedParser;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class SpecialTests
    {
        private const string ResourceId = "Special_";
        private Dictionary<string, (string name, string file)> resources;

        public SpecialTests()
        {
            //Get Special Resources
            resources = Resources.GetResources(ResourceId);
        }

        [TestMethod]
        public async Task Special_Nordigt_Online()
        {
            //Init
            var feed = await FeedParser.ParseFeedAsync(@"https://feed.khz.se/nordigt", CancellationToken.None);

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Special_Nordigt() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}