namespace DKW.Abp.AspNetCore;

public enum Breakpoint
{
    /// <summary>
    /// Width &lt; 576px
    /// </summary>
    XS,
    /// <summary>
    /// Width &lt; 768
    /// </summary>
    SM,
    /// <summary>
    /// Width &lt; 992
    /// </summary>
    MD,
    /// <summary>
    /// Width &lt; 1200
    /// </summary>
    LG,
    /// <summary>
    /// Width &lt; 1400
    /// </summary>
    XL,
    /// <summary>
    /// Width &gt;= 1400
    /// </summary>
    XXL
}

public enum ColorClass
{
    None,
    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
    Info,
    Light,
    Dark
}

public enum ButtonClass
{
    None,
    Small,
    Large
}

public enum ButtonType
{
    Button,
    Submit,
    Reset
}

public static partial class BootstrapExtensions
{
    public static String ButtonType(this ButtonType buttonType) => buttonType switch
    {
        AspNetCore.ButtonType.Submit => "submit",
        AspNetCore.ButtonType.Reset => "reset",
        _ => "button"
    };

    public static String Alert(this ColorClass colorClass) => colorClass switch
    {
        ColorClass.Primary => "alert-primary",
        ColorClass.Secondary => "alert-secondary",
        ColorClass.Success => "alert-success",
        ColorClass.Danger => "alert-danger",
        ColorClass.Warning => "alert-warning",
        ColorClass.Info => "alert-info",
        ColorClass.Light => "alert-light",
        ColorClass.Dark => "alert-dark",
        _ => String.Empty
    };

    public static String Bg(this ColorClass colorClass) => colorClass switch
    {
        ColorClass.Primary => "bg-primary",
        ColorClass.Secondary => "bg-secondary",
        ColorClass.Success => "bg-success",
        ColorClass.Danger => "bg-danger",
        ColorClass.Warning => "bg-warning",
        ColorClass.Info => "bg-info",
        ColorClass.Light => "bg-light",
        ColorClass.Dark => "bg-dark",
        _ => String.Empty
    };

    public static String Fg(this ColorClass colorClass) => colorClass switch
    {
        ColorClass.Primary => "text-light",
        ColorClass.Secondary => "text-light",
        ColorClass.Success => "text-light",
        ColorClass.Danger => "text-light",
        ColorClass.Warning => "text-dark",
        ColorClass.Info => "text-dark",
        ColorClass.Light => "text-dark",
        ColorClass.Dark => "text-light",
        _ => String.Empty
    };

    public static String Badge(this ColorClass colorClass) => colorClass switch
    {
        ColorClass.Primary => "bg-primary",
        ColorClass.Secondary => "bg-secondary",
        ColorClass.Success => "bg-success",
        ColorClass.Danger => "bg-danger",
        ColorClass.Warning => "bg-warning text-dark",
        ColorClass.Info => "bg-info text-dark",
        ColorClass.Light => "bg-light text-dark",
        ColorClass.Dark => "bg-dark",
        _ => String.Empty
    };

    public static String Css(this ButtonClass buttonClass, ColorClass colorClass = ColorClass.None, Boolean outline = false)
    {
        var color = outline ? colorClass.ButtonOutline() : colorClass.Button();

        return (buttonClass switch
        {
            ButtonClass.Small => $"btn btn-sm {color}",
            ButtonClass.Large => $"btn btn-lg {color}",
            _ => $"btn {color}"
        }).Trim();
    }

    public static String Button(this ColorClass colorClass) => colorClass switch
    {
        ColorClass.Primary => "btn-primary",
        ColorClass.Secondary => "btn-secondary",
        ColorClass.Success => "btn-success",
        ColorClass.Danger => "btn-danger",
        ColorClass.Warning => "btn-warning",
        ColorClass.Info => "btn-info",
        ColorClass.Light => "btn-light",
        ColorClass.Dark => "btn-dark",
        _ => String.Empty
    };

    public static String ButtonOutline(this ColorClass colorClass) => colorClass switch
    {
        ColorClass.Primary => "btn-outline-primary",
        ColorClass.Secondary => "btn-outline-secondary",
        ColorClass.Success => "btn-outline-success",
        ColorClass.Danger => "btn-outline-danger",
        ColorClass.Warning => "btn-outline-warning",
        ColorClass.Info => "btn-outline-info",
        ColorClass.Light => "btn-outline-light",
        ColorClass.Dark => "btn-outline-dark",
        _ => String.Empty
    };

    public static String Text(this ColorClass colorClass, String? cssClass = null)
    {
        var color = colorClass switch
        {
            ColorClass.Primary => "text-primary",
            ColorClass.Secondary => "text-secondary",
            ColorClass.Success => "text-success",
            ColorClass.Danger => "text-danger",
            ColorClass.Warning => "text-warning",
            ColorClass.Info => "text-info",
            ColorClass.Light => "text-light",
            ColorClass.Dark => "text-dark",
            _ => String.Empty
        };

        return String.Join(" ", cssClass, color).Trim();
    }

    public static String Border(this ColorClass colorClass, String? cssClass = null)
    {
        var color = colorClass switch
        {
            ColorClass.Primary => "border-primary",
            ColorClass.Secondary => "border-secondary",
            ColorClass.Success => "border-success",
            ColorClass.Danger => "border-danger",
            ColorClass.Warning => "border-warning",
            ColorClass.Info => "border-info",
            ColorClass.Light => "border-light",
            ColorClass.Dark => "border-dark",
            _ => String.Empty
        };

        return String.Join(" ", cssClass, color).Trim();
    }

    public static String Icon(this ColorClass colorClass) => colorClass switch
    {
        ColorClass.Primary => "fa-duotone fa-certificate",
        ColorClass.Secondary => "fa-duotone fa-badge",
        ColorClass.Success => "fa-duotone fa-check-circle",
        ColorClass.Danger => "fa-duotone fa-exclamation-circle",
        ColorClass.Warning => "fa-duotone fa-question-circle",
        ColorClass.Info => "fa-duotone fa-info-circle",
        ColorClass.Light => "fa-duotone fa-lightbulb-on",
        ColorClass.Dark => "fa-duotone fa-lightbulb",
        _ => String.Empty
    };
}
