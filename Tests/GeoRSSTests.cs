using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class GeoRSSTests
    {
        private const string ResourceId = "GeoRSS_";
        private Dictionary<string, (string name, string file)> resources;

        public GeoRSSTests()
        {
            //Get GeoRSS Resources
            resources = Resources.GetResources(ResourceId);
        }

        [TestMethod]
        public async Task GeoRSS_SIMPLE_Single_Item_Point() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task GeoRSS_GML_Single_Item_Point() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task GeoRSS_GML_Feed_Line_Multiple_Item_Point() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}