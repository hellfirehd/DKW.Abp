using Microsoft.Extensions.Hosting;

namespace Inara.Icons.Generator;

internal static class Program
{
    private const String SvgPathFree = "IconLibraries:FAF:SourcePath";
    private const String OutputPathFree = "IconLibraries:FAF:OutputPath";
    private const String SvgPathPro = "IconLibraries:FAP:SourcePath";
    private const String OutputPathPro = "IconLibraries:FAP:OutputPath";

    private static async Task<Int32> Main(String[] args)
    {

        var builder = Host.CreateApplicationBuilder(args);

        await new FontAwesomeFree().Generate(builder.Configuration[SvgPathFree]!, builder.Configuration[OutputPathFree]!).ConfigureAwait(false);

        await new FontAwesomePro().Generate(builder.Configuration[SvgPathPro]!, builder.Configuration[OutputPathPro]!).ConfigureAwait(false);

        return 0;
    }
}