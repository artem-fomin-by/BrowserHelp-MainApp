using System.Text.RegularExpressions;

namespace Logic;

public static class WildcardUtilities
{
    public static Regex WildcardToRegex(string wildcard)
    {
        var regexPattern = Regex.Escape(wildcard).Replace("\\*", ".*");
        return new Regex(regexPattern);
    }
}
