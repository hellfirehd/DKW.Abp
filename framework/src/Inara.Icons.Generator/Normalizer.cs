using System.Text;
using System.Text.RegularExpressions;

namespace Inara.Icons.Generator;

public static partial class Normalizer
{
    public static String MakeSafe(this String name)
    {
        var key = name.Replace(".svg", String.Empty).Trim();

        if (String.Equals(key, "Equals", StringComparison.OrdinalIgnoreCase))
        {
            key = "EqualsSign";
        }

        if (Char.IsAsciiDigit(key[0]))
        {
            key = $"N{key}";
        }

        var parts = key.Split('-');
        var sb = new StringBuilder();
        foreach (var part in parts)
        {
            sb.Append(Char.ToUpper(part[0]));
            sb.Append(part[1..]);
        }

        return sb.ToString();
    }

    public static String SVG(this String svg)
    {
        svg = SvgTag().Replace(svg, "$1");
        svg = XmlComment().Replace(svg, String.Empty);
        svg = svg.Replace("\"", "\\\"");
        return svg.Trim();
    }

    [GeneratedRegex("<!--.*?-->")]
    private static partial Regex XmlComment();

    [GeneratedRegex("<svg.*?>(.*?)</svg>")]
    private static partial Regex SvgTag();
}