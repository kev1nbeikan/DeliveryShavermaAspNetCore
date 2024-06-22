namespace HandlerService.Infustucture.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string? str)
    {
        return !string.IsNullOrEmpty(str);
    }

    public static string MakeEmptyIfNull(this string? str)
    {
        return string.IsNullOrEmpty(str)
            ? string.Empty
            : str;
    }
}