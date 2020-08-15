using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class AtomTests
    {
        private const string ResourceId = "Atom_";
        private Dictionary<string, (string name, string file)> resources;

        public AtomTests()
        {
            //Get Atom Resources
            resources = Resources.GetResources(ResourceId);
        }

        [TestMethod]
        public async Task Atom_Basic() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task Atom_Extended() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task Atom_Extended_II() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}