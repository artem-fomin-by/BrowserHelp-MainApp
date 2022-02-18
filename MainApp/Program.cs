using System.Diagnostics;

using Logic;
using Logic.BrowserLogic;
using MainApp.AppWindows;
using WinFormsLogic;

namespace MainApp{
    public static class Program{
        public const string AppName = "BrowserHelper";

        public const string OrigHTTPProgIDValueName = "OrigHTTPProgID";
        public const string OrigHTTPSProgIDValueName = "OrigHTTPSProgID";

        public const string AppProgID = "BrowserHelper.ID";

        public const string STD_InstallCommand = "-install";
        public const string STD_DeleteCommand = "-delete";

        [STAThread]
        public static void Main(string[] args){
            if(args.Length > 0 && args[0].Equals(STD_InstallCommand)){
                Install();
                return;
            }

            if(args.Length > 0 && args[0].Equals(STD_DeleteCommand)){
                Remove(args.Length > 1 ? args[1] : null, args.Length > 2 ? args[2] : null);
                return;
            }

            ApplicationConfiguration.Initialize();

            BrowserButton[] FoundBrowsers;
            try{
                FoundBrowsers = BrowserServ.FindBrowsers(AppName).Select(
                    x => new BrowserButton(x)).ToArray();
            }
            catch(Exception exception){
                Application.Run(new ErrorWindow(AppName, exception));
                return;
            }

            if(FoundBrowsers != null && FoundBrowsers.Length != 0){
                Application.Run(new MainWindow(FoundBrowsers, AppName, args.Length > 0 ? args[0] : ""));
            }
            else{
                Application.Run(new NoBrowsersWindow(AppName));
            }
        }

        private static void Install(){
            var appKey = WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath).CreateSubKey(AppName);
            var appProgIDKey = WorkWithReg.GetKey(WorkWithReg.ProgIDsPath).CreateSubKey(AppProgID);

            WorkWithReg.CreateRegistryKeysTree(appKey, BrowserServ.BrowserLaunchCommandPath)
                .SetValue(BrowserServ.LaunchCommandValueName, Application.ExecutablePath);
            WorkWithReg.CreateRegistryKeysTree(appProgIDKey, BrowserServ.BrowserLaunchCommandPath)
                .SetValue(BrowserServ.LaunchCommandValueName, Application.ExecutablePath);

            var defaultBrowserKey1 = WorkWithReg.GetKey(BrowserServ.HTTPDefaultBrowserKeyPath);
            appKey.SetValue(OrigHTTPProgIDValueName, defaultBrowserKey1.GetValue("ProgID"));
            defaultBrowserKey1.SetValue("ProgId", AppProgID);

            var defaultBrowserKey2 = WorkWithReg.GetKey(BrowserServ.HTTPSDefaultBrowserKeyPath);
            appKey.SetValue(OrigHTTPSProgIDValueName, defaultBrowserKey2.GetValue("ProgID"));
            defaultBrowserKey2.SetValue("ProgID", AppProgID);
        }

        private static void Remove(string? defaultBrowserProgID1, string? defaultBrowserProgID2){
            var appKey = WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath).OpenSubKey(AppName);

            defaultBrowserProgID1 = defaultBrowserProgID1 != null ? defaultBrowserProgID1 :
                (string?) appKey.GetValue(OrigHTTPProgIDValueName);
            defaultBrowserProgID2 = defaultBrowserProgID2 != null ? defaultBrowserProgID2 :
                (string?) appKey.GetValue(OrigHTTPSProgIDValueName);
            WorkWithReg.GetKey(BrowserServ.HTTPDefaultBrowserKeyPath).SetValue("ProgID", defaultBrowserProgID1);
            WorkWithReg.GetKey(BrowserServ.HTTPSDefaultBrowserKeyPath).SetValue("ProgID", defaultBrowserProgID2);
            
            WorkWithReg.DeleteKey(WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath), AppName);
            WorkWithReg.DeleteKey(WorkWithReg.GetKey(WorkWithReg.ProgIDsPath), AppProgID);

            var appPath = Application.ExecutablePath;
            var appEXEFileName = appPath.Split(@"\")[^1];
            var appDirectoryPath = appPath.Substring(0, appPath.Length - appEXEFileName.Length - 1);

            Process.Start("cmd.exe", "rm " + appDirectoryPath + " -r");
        }
    }
}
