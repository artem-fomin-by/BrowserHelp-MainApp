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
        var BrowsersKey = WorkWithReg.GetKey(BrowsersKeyPath);
        var BrowsersNames = BrowsersKey
            .GetSubKeyNames()
            .Where(x => !x.Equals(SystemBrowser) && !x.Equals(programKeyName));

        return BrowsersNames
            .Select(x => new Browser(
                x, (string)WorkWithReg
                    .GetKey(BrowserLaunchCommandPath, BrowsersKey.OpenSubKey(x))
                    .GetValue(LaunchCommandValueName)))
            .ToArray();
    }

    public string Name { get; set; }
    public string LaunchCommand { get; set; }
    public string LaunchCommandArgs { get; set; }

    public Browser()
    {

    }

    public Browser(string name, string launchCommand, string args = "")
    {
        Name = name;
        LaunchCommand = launchCommand;
        LaunchCommandArgs = args;
    }

    public void Launch(string link = "")
    {
        Process.Start(LaunchCommand, $"{link} {LaunchCommandArgs}");
    }
}
