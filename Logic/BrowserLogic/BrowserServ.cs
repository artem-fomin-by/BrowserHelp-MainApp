namespace Logic.BrowserLogic{
    public static class BrowserServ{
        #region RegistryKeyPathes

        public const string BrowsersKeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet";
        public const string BrowserLaunchCommandPath = @"\shell\open\command";
        public const string LaunchCommandValueName = "";

        #endregion

        private const string SystemBrowser = "IEXPLORE.EXE"; // Browser to ignore

        public static Browser[] FindBrowsers(string programKeyName){
            var BrowsersKey = WorkWithReg.GetKey(BrowsersKeyPath);
            var BrowsersNames = BrowsersKey
                .GetSubKeyNames()
                .Where(x => !x.Equals(SystemBrowser) && !x.Equals(programKeyName));

            return BrowsersNames
                .Select(x => new Browser(
                    x, (string) WorkWithReg
                        .GetKey(BrowserLaunchCommandPath, BrowsersKey.OpenSubKey(x))
                        .GetValue(LaunchCommandValueName)))
                .ToArray();
        }
    }
}
