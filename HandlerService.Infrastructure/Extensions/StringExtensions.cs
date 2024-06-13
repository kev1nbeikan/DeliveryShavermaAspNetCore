namespace HandlerService.Infustucture.Extensions;

public static class StringExtensions
{
    public static bool IsNotEmptyOrNull(this string? str)
    {
        return !string.IsNullOrEmpty(str);
    }
}