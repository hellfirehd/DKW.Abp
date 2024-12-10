using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class SafeAssemblies
{
    public static Boolean Predicate(Assembly a)
    {
        if (a.FullName?.StartsWith("Microsoft") == true)
        {
            return false;
        }

        if (a.FullName?.StartsWith("System") == true)
        {
            return false;
        }

        if (a.FullName?.StartsWith("Volo") == true)
        {
            return false;
        }

        return true;
    }
}