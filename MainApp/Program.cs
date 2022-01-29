using Logic.BrowserLogic;
using MainApp.AppWindows;
using WinFormsLogic;

namespace MainApp{
    public static class Program{
        private const string AppName = "BrowserHelper";

        private const string STD_InstallCommand = "install";
        private const string STD_DeleteCommand = "delete";

        [STAThread]
        public static void Main(string[] args){
            if(args.Length > 0 && args[0].Equals(STD_InstallCommand)){
                Installer.Install(AppName);
                return;
            }

            if(args.Length > 0 && args[0].Equals(STD_DeleteCommand)){
                Remover.Remove(AppName);
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
    }
}
