using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tests.Helpers
{
    internal static class Resources
    {
        private static Assembly assembly = Assembly.GetExecutingAssembly();

        internal static Dictionary<string, (string name, string file)> GetResources(string resourceId)
        {
            //Get resources, starting with resource id
            var names = assembly.GetManifestResourceNames();
            return names.Where(name => name.Contains(resourceId))
                .Select(name =>
                {
                    var startIndex = name.LastIndexOf(resourceId);
                    return (name, file: name.Substring(startIndex));
                })
                .ToDictionary(resource =>
                {
                    var extension = Path.GetExtension(resource.file);
                    return resource.file.Substring(0, resource.file.Length - extension.Length);
                });
        }

        internal static Stream GetResource(string resource) => assembly.GetManifestResourceStream(resource);

        internal static string Location => Path.Combine(Directory.GetParent(assembly.Location).Parent.Parent.Parent.FullName, "Resources");
    }
}