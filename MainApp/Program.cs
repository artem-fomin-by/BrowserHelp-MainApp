using Logic;
using Logic.BrowserLogic;
using MainApp.MainWindowDir;
using WinFormsLogic;

namespace MainApp{
    public static class Program{
        private const string AppName = "BrowserHelper";

        //ToDo Make RegKeyNameToIgnore
        [STAThread]
        public static void Main(string[] args){
            if(args.Length > 0 && args[0].Equals("install")){
                Installer.Install(AppName);
                return;
            }

            if(args.Length > 0 && (args[0].Equals("Delete") || args[0].Equals("Remove"))){
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
                Application.Run(new MainWindow(AppName, exception));
                return;
            }

            Application.Run(new MainWindow(FoundBrowsers, AppName, args.Length > 0 ? args[0] : ""));
        }
    }
}
