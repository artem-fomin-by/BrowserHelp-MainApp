using Logic.BrowserLogic;
using MainApp.AppWindows;
using WinFormsLogic;

namespace MainApp{
    public static class Program{
        public const string AppName = "BrowserHelper";

        [STAThread]
        public static void Main(string[] args){

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
