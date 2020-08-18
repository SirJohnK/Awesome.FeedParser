using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    /// <summary>
    /// Tests for different Geo RSS feeds.
    /// </summary>
    [TestClass]
    public class GeoRSSTests
    {
        private const string ResourceId = "GeoRSS_";
        private Dictionary<string, (string name, string file)> resources;

        /// <summary>
        /// Setup and get Geo RSS resources.
        /// </summary>
        public GeoRSSTests()
        {
            //Get GeoRSS Resources
            resources = Resources.GetResources(ResourceId);
        }

        /// <summary>
        /// Parse and verify SIMPLE single item point Geo RSS feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task GeoRSS_SIMPLE_Single_Item_Point() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify GML single item point Geo RSS feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task GeoRSS_GML_Single_Item_Point() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify GML multiple item point Geo RSS feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task GeoRSS_GML_Feed_Line_Multiple_Item_Point() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}