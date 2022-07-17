using System.Diagnostics;

namespace Logic;

public class Browser
{
    #region RegistryKeyPathes

    public const string BrowsersKeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet";
    public const string BrowserLaunchCommandPath = @"\shell\open\command";
    public const string LaunchCommandValueName = "";

    #endregion

    private const string SystemBrowser = "IEXPLORE.EXE"; // Browser to ignore

    public static Browser[] FindBrowsers(string programKeyName)
    {
        using var browsersKey = WorkWithReg.GetKey(BrowsersKeyPath);
        var browsersNames = browsersKey
            .GetSubKeyNames()
            .Where(x => !x.Equals(SystemBrowser) && !x.Equals(programKeyName));

        return browsersNames
            .Select(x => new Browser
            {
                Name = x,
                LaunchCommand =
                    (string)WorkWithReg
                        .GetKey(BrowserLaunchCommandPath, browsersKey.OpenSubKey(x))
                        .GetValue(LaunchCommandValueName)
            })
            .ToArray();
    }

    public string Name { get; set; }
    public string LaunchCommand { get; set; }
    public string LaunchCommandArgs { get; set; }
    public string[]? Applications { get; set; }
    public string[]? UrlPatterns { get; set; }

    public void Launch(string link = "")
    {
        Process.Start(LaunchCommand, $"{link} {LaunchCommandArgs}");
    }

    public bool IsLinkMatch(string link)
    {
        string linkToCheck = link;
        if(!link.EndsWith('/')) linkToCheck = link + '/';

        return UrlPatterns?.Select(WildcardUtilities.WildcardToRegex).Any(x => x.IsMatch(linkToCheck)) == true;
    }
}
