using Logic.BrowserLogic;

namespace SystemConsoleApp{
    public static class Program{
        public static void Main(){
            BrowserServ.FindBrowsers("")[0].Launch();
        }
    }
}
