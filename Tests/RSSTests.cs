using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    /// <summary>
    /// Tests for different RSS feeds.
    /// </summary>
    [TestClass]
    public class RSSTests
    {
        private const string ResourceId = "RSS_";
        private Dictionary<string, (string name, string file)> resources;

        /// <summary>
        /// Setup and get RSS resources.
        /// </summary>
        public RSSTests()
        {
            //Get RSS Resources
            resources = Resources.GetResources(ResourceId);
        }

        /// <summary>
        /// Parse and verify RSS version 0.91 RSS feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task RSS_0_91() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify RSS version 0.92 RSS feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task RSS_0_92() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify RSS version 1.0 RSS feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task RSS_1_0() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify extended RSS version 1.0 RSS feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task RSS_1_0_Extended() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify RSS version 2.0 RSS feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task RSS_2_0() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}