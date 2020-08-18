using Awesome.FeedParser;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    /// <summary>
    /// Tests for different custom feeds.
    /// </summary>
    [TestClass]
    public class SpecialTests
    {
        private const string ResourceId = "Special_";
        private Dictionary<string, (string name, string file)> resources;

        /// <summary>
        /// Setup and get custom/special resources.
        /// </summary>
        public SpecialTests()
        {
            //Get Special Resources
            resources = Resources.GetResources(ResourceId);
        }

        /// <summary>
        /// Parse and verify the current live/online feed of the podcast "Nördigt".
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Special_Nordigt_Online()
        {
            //Init
            var feed = await FeedParser.ParseFeedAsync(@"https://feed.khz.se/nordigt", CancellationToken.None);

            //Assert
            feed.Should().NotBeNull();
        }

        /// <summary>
        /// Parse and verify a offline feed of the podcast "Nördigt".
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Special_Nordigt() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}