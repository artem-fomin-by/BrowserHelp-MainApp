using Logic;
using Logic.BrowserLogic;
using WinFormsLogic;

namespace MainApp{
    public static class Program{
        private const string AppName = "";

        [STAThread]
        public static void Main(){
            var inp = Environment.GetCommandLineArgs()[0];
            if(inp.Equals("install")){
                Installer.Install(AppName);
                return;
            }

            if(inp.Equals("Delete") || inp.Equals("Remove")){
                Remover.Remove(AppName);
                return;
            }

            var FoundBrowsers = BrowserServ.FindBrowsers(AppName).Select(
                x => new BrowserButton(x, inp)).ToArray();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(FoundBrowsers, AppName));
        }
    }
}
