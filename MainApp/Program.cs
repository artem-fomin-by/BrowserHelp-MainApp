using Logic;
using Logic.BrowserLogic;
using WinFormsLogic;

namespace MainApp{
    public static class Program{
        private const string AppName = "BrowserHelper";

        //ToDo Make RegKeyNameToIgnore
        [STAThread]
        public static void Main(string[] args){
            if(args[0].Equals("install")){
                Installer.Install(AppName);
                return;
            }

            if(args[0].Equals("Delete") || args[0].Equals("Remove")){
                Remover.Remove(AppName);
                return;
            }

            ApplicationConfiguration.Initialize();

            var FoundBrowsers = BrowserServ.FindBrowsers(AppName).Select(
                x => new BrowserButton(x, args[0])).ToArray();

            Application.Run(new Form1(FoundBrowsers, AppName));
        }
    }
}
