using Logic;
using Logic.BrowserLogic;
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

            var FoundBrowsers = BrowserServ.FindBrowsers(AppName).Select(
                x => new BrowserButton(x)).ToArray();

            Application.Run(new Form1(FoundBrowsers, AppName, args.Length > 0 ? args[0] : ""));
        }
    }
}
