using System.Diagnostics;

using Logic;
using Logic.BrowserLogic;
using MainApp.AppWindows;
using WinFormsLogic;

namespace MainApp{
    public static class Program{
        private const string AppName = "BrowserHelper";
        
        private const string OldProgID1ValueName = "OldProgID1";
        private const string OldProgID2ValueName = "OldProgID2";

        private const string AppProgID = "BrowserHelper.ID";

        private const string STD_InstallCommand = "-install";
        private const string STD_DeleteCommand = "-delete";

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

            var defaultBrowserKey1 = WorkWithReg.GetKey(BrowserServ.DefaultBrowserKeyPath1);
            appKey.SetValue(OldProgID1ValueName, defaultBrowserKey1.GetValue("ProgID"));
            defaultBrowserKey1.SetValue("ProgId", AppProgID);

            var defaultBrowserKey2 = WorkWithReg.GetKey(BrowserServ.DefaultBrowserKeyPath2);
            appKey.SetValue(OldProgID2ValueName, defaultBrowserKey2.GetValue("ProgID"));
            defaultBrowserKey2.SetValue("ProgID", AppProgID);
        }

        private static void Remove(string? defaultBrowserProgID1, string? defaultBrowserProgID2){
            var appKey = WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath).OpenSubKey(AppName);

            defaultBrowserProgID1 = defaultBrowserProgID1 != null ? defaultBrowserProgID1 :
                (string?) appKey.GetValue(OldProgID1ValueName);
            defaultBrowserProgID2 = defaultBrowserProgID2 != null ? defaultBrowserProgID2 :
                (string?) appKey.GetValue(OldProgID2ValueName);
            WorkWithReg.GetKey(BrowserServ.DefaultBrowserKeyPath1).SetValue("ProgID", defaultBrowserProgID1);
            WorkWithReg.GetKey(BrowserServ.DefaultBrowserKeyPath1).SetValue("ProgID", defaultBrowserProgID2);
            
            WorkWithReg.DeleteKey(WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath), AppName);
            WorkWithReg.DeleteKey(WorkWithReg.GetKey(WorkWithReg.ProgIDsPath), AppProgID);

            var appPath = Application.ExecutablePath;
            var appEXEFileName = appPath.Split(@"\")[^1];
            var appDirectoryPath = appPath.Substring(0, appPath.Length - appEXEFileName.Length - 1);

            Process.Start("cmd.exe", "rm " + appDirectoryPath + " -r");
        }
    }
}
