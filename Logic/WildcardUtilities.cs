using System.Text.RegularExpressions;

namespace Logic;

public static class WildcardUtilities
{
    public static Regex WildcardToRegex(string wildcard)
    {
        var start = wildcard.StartsWith('*') ? "" : "^";
        var end = "";
        if(wildcard.EndsWith("/")) end = "$";
        else if(!wildcard.EndsWith("/*")) end = ".*";

        var regexPattern = 
            string.Concat(start, Regex.Escape(wildcard).Replace("\\*", ".*"), end);
        return new Regex(regexPattern);
    }
}
