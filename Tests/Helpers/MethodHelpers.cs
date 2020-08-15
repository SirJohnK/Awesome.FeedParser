using System.Runtime.CompilerServices;

namespace Tests.Helpers
{
    internal static class MethodHelpers
    {
        internal static string GetName([CallerMemberName] string memberName = "") => memberName;
    }
}