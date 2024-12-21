namespace Microsoft.Extensions.Configuration;

public static class DkwConnectionStringConfigurationExtensions
{
    public static String GetConnectionStringOrDefault(this IConfiguration configuration, String name)
    {
        return configuration.GetConnectionString(name)
            ?? configuration.GetConnectionString("Default")
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException($"A Connection string named '{name}' was not found in the configuration. Connection strings named 'Default' and 'DefaultConnection' were also not found.");
    }
}
