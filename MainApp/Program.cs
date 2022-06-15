using Logic;
using MainApp.AppWindows;
using WinFormsLogic;

namespace MainApp;

public static class Program{
    public const string AppName = "MainApp";

    [STAThread]
    public static void Main(string[] args){

        ApplicationConfiguration.Initialize();

        var FoundBrowsers = BrowserServ.FindBrowsers(AppName).Select(
            x => new BrowserButton(x)).ToArray();

        if(FoundBrowsers != null && FoundBrowsers.Length != 0){
            Application.Run(new MainWindow(FoundBrowsers, AppName, args.Length > 0 ? args[0] : ""));
        }
        else{
            Application.Run(new NoBrowsersWindow(AppName));
        }
    }
}
